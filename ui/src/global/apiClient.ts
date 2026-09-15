import axios from "axios";

const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

let isRefreshing = false;
let refreshQueue: Array<{
  resolve: (value: string | null) => void;
  reject: (reason?: unknown) => void;
}> = [];

const processRefreshQueue = (token: string | null) => {
  refreshQueue.forEach(({ resolve, reject }) => {
    if (token) {
      resolve(token);
      return;
    }

    reject(new Error("Refresh failed"));
  });
  refreshQueue = [];
};

apiClient.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("access-token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

apiClient.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error?.config;

    if (error?.response?.status === 401 && !originalRequest._retry) {
      const refreshToken = localStorage.getItem("refresh-token");

      if (!refreshToken) {
        localStorage.removeItem("access-token");
        localStorage.removeItem("refresh-token");
        window.location.href = "/login";
        return Promise.reject(error);
      }

      originalRequest._retry = true;

      if (!isRefreshing) {
        isRefreshing = true;

        try {
          const refreshResponse = await axios.post(
            `${import.meta.env.VITE_API_BASE_URL}/auth/refresh`,
            { refreshToken },
            {
              headers: {
                "Content-Type": "application/json",
              },
            }
          );

          const newAccessToken = refreshResponse?.data?.value?.accessToken ?? refreshResponse?.data?.accessToken;
          const newRefreshToken = refreshResponse?.data?.value?.refreshToken ?? refreshResponse?.data?.refreshToken;

          if (!newAccessToken) {
            throw new Error("No access token returned from refresh call");
          }

          localStorage.setItem("access-token", newAccessToken);
          if (newRefreshToken) {
            localStorage.setItem("refresh-token", newRefreshToken);
          }

          processRefreshQueue(newAccessToken);
          isRefreshing = false;

          originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
          return apiClient(originalRequest);
        } catch (refreshError) {
          isRefreshing = false;
          processRefreshQueue(null);
          localStorage.removeItem("access-token");
          localStorage.removeItem("refresh-token");
          window.location.href = "/login";
          return Promise.reject(refreshError);
        }
      }

      return new Promise((resolve, reject) => {
        refreshQueue.push({
          resolve: (token) => {
            if (!token) {
              reject(error);
              return;
            }

            originalRequest.headers.Authorization = `Bearer ${token}`;
            resolve(apiClient(originalRequest));
          },
          reject,
        });
      });
    }

    return Promise.reject(error);
  }
);

export default apiClient;
