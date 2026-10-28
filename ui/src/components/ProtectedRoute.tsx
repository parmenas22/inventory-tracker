import { getUserRoles } from "@/lib/auth";
import { useAuth } from "@/context/AuthContext";
import { Navigate, Outlet } from "react-router-dom";

export const ProtectedRoute = ({ requiredRole }: { requiredRole?: string }) => {
  const { token, user } = useAuth();
  if (!token) {
    return <Navigate to="/login" replace />;
  }

  if (requiredRole) {
    const roles = getUserRoles(user);
    if (!roles.includes(requiredRole)) {
      return <Navigate to="/" replace />;
    }
  }

  return <Outlet />;
};
