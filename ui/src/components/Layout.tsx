import Header from "./Header";
import { Outlet } from "react-router-dom";

const Layout = () => {
  return (
    <div className="min-h-screen bg-gray-50">
      <Header />
      <main className="p-6">
        <Outlet />
      </main>
    </div>
  );
};

export default Layout;
