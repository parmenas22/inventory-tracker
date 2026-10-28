import { useAuth } from "@/context/AuthContext";
import { Button } from "./ui/button";
import { LogOut } from "lucide-react";

const Header = () => {
  const { user, logout } = useAuth();
  return (
    <header className="flex min-h-20 items-center justify-between border-b border-slate-200 bg-white px-4 sm:px-6 lg:px-8">
      <p className="text-sm text-slate-600">
        Welcome back{user?.FirstName ? `, ${user.FirstName}` : ""} 👋
      </p>
      <div className="flex items-center">
        <Button
          variant="ghost"
          onClick={logout}
          className="text-slate-600 hover:text-slate-900"
        >
          <LogOut className="h-4 w-4" />
          Logout
        </Button>
      </div>
    </header>
  );
};

export default Header;
