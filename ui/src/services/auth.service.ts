import apiClient from "@/global/apiClient";

export interface ApiResponse<T = any> {
  succeeded: boolean;
  message: string;
  statusCode: number;
  error: unknown | null;
  value: T | null;
  timestamp: string;
}

export const authService = {
  async forgotPassword(email: string): Promise<ApiResponse<{ resetToken?: string }>> {
    const res = await apiClient.post<ApiResponse<{ resetToken?: string }>>(
      "/auth/forgot-password",
      { email }
    );
    return res.data;
  },

  async resetPassword(
    resetToken: string,
    newPassword: string
  ): Promise<ApiResponse> {
    const res = await apiClient.post<ApiResponse>("/auth/reset-password", {
      resetToken,
      newPassword,
    });
    return res.data;
  },

  async register(data: {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
  }): Promise<ApiResponse> {
    const res = await apiClient.post<ApiResponse>("/auth/sign-up", data);
    return res.data;
  },
};
