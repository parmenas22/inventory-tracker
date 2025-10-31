import { useAuth } from "@/context/AuthContext";
import { Button } from "./ui/button";
import { LogOut, Package } from "lucide-react";

const Header = () => {
  const { user, logout } = useAuth();
  return (
    <header className="flex justify-between items-center bg-white shadow-sm p-4 rounded-xl mb-6">
      <div className="flex items-center gap-3">
        <div className="bg-primary/10 p-2.5 rounded-xl">
          <Package className="h-6 w-6 text-primary" />
        </div>
        <div>
          <h1 className="text-2xl font-semibold text-foreground">
            Inventory Tracker
          </h1>
          <p className="text-sm text-muted-foreground">
            Manage your stock with ease
          </p>
        </div>
      </div>

      <div className="flex items-center gap-4">
        <span className="text-gray-700">
          Welcome back{user?.FirstName ? `, ${user.FirstName}` : ""} 👋
        </span>
        <Button
          onClick={logout}
          className="bg-gray-100 hover:bg-gray-200 text-gray-800 px-3 py-1 rounded-lg text-sm transition"
        >
          <LogOut className="h-4 w-4 mr-2" />
          Logout
        </Button>
      </div>
    </header>
  );
};

export default Header;
