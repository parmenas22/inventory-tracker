import {
  createContext,
  useContext,
  useEffect,
  useState,
  type ReactNode,
} from "react";
import { jwtDecode } from "jwt-decode";
import type { LoginSchema } from "@/validators/loginSchema";
import type { ApiResponse } from "@/services/auth.service";
import apiClient from "@/global/apiClient";

interface DecodedToken {
  FirstName: string;
  LastName: string;
  UserId: string;
  email: string;
  iss: string;
  jti: string;
  roles: string[];
}

interface AuthContextType {
  token: string | null;
  isAuthenticated: boolean;
  logout: () => void;
  login: (data: LoginSchema) => Promise<ApiResponse>;
  user: DecodedToken | null;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState<DecodedToken | null>(null);
  const [token, setToken] = useState(localStorage.getItem("access-token"));

  const login = async (data: LoginSchema): Promise<ApiResponse> => {

    try {
      const res = await apiClient.post<ApiResponse>("/auth/login", data);

      if (res.data?.succeeded) {
        const accessToken = res.data?.value?.accessToken;
        const refreshToken = res.data?.value?.refreshToken;

        if (accessToken) {
          setToken(accessToken);
        }

        if (refreshToken) {
          localStorage.setItem("refresh-token", refreshToken);
        }
      }

      return res.data;
    } catch (error: any) {
      console.error("Login request failed:", error);
      const backendMessage =
        error?.response?.data?.message ??
        error?.response?.data?.error?.message ??
        "Login failed";

      return {
        succeeded: false,
        message: backendMessage,
        statusCode: error?.response?.status ?? 500,
        error: error?.response?.data ?? error,
        value: null,
        timestamp: new Date().toISOString(),
      };
    }
  };

  useEffect(() => {
    if (token) {
      localStorage.setItem("access-token", token);
      const decoded = jwtDecode<DecodedToken>(token);
      setUser(decoded);
      setIsAuthenticated(true);
    } else {
      logout();
    }
  }, [token]);

  const logout = () => {
    localStorage.removeItem("access-token");    localStorage.removeItem('refresh-token');    setToken(null);
    setIsAuthenticated(false);
    setUser(null);
  };

  return (
    <AuthContext.Provider
      value={{ token, isAuthenticated, login, logout, user }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth must be used within AuthProvider");
  return context;
};
