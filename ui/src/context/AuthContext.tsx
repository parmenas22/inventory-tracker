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
    const res = await apiClient.post<ApiResponse>("/auth/login", data);
    if (res.data?.succeeded && res.data?.value?.token) {
      setToken(res.data.value.token);
    }

    return res.data;
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
    localStorage.removeItem("access-token");
    setToken(null);
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
