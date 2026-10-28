import Header from "./Header";
import { NavLink, Outlet } from "react-router-dom";
import {
  LayoutDashboard,
  Package,
  PanelLeftClose,
  PanelLeftOpen,
  UserPlus,
} from "lucide-react";
import { getUserRoles } from "@/lib/auth";
import { useAuth } from "@/context/AuthContext";
import { Button } from "./ui/button";
import { useState } from "react";

const Layout = () => {
  const { user } = useAuth();
  const [isExpanded, setIsExpanded] = useState(true);
  const roles = getUserRoles(user);
  const isAdmin = roles.includes("Admin");

  return (
    <div className="min-h-screen bg-slate-50">
      <aside
        className={`border-b border-slate-200 bg-white transition-[width] duration-200 md:fixed md:inset-y-0 md:left-0 md:z-10 md:flex md:flex-col md:border-b-0 md:border-r ${
          isExpanded ? "md:w-64" : "md:w-20"
        }`}
      >
        <div
          className={`flex items-center px-4 py-5 ${
            isExpanded ? "justify-between gap-3" : "justify-center"
          }`}
        >
          <div
            className={`flex min-w-0 items-center gap-3 ${
              isExpanded ? "" : "hidden"
            }`}
          >
            <div className="shrink-0 rounded-xl bg-primary/10 p-2.5">
              <Package className="h-6 w-6 text-primary" aria-hidden="true" />
            </div>
            <div className="min-w-0">
              <h1 className="truncate text-lg font-semibold text-foreground">
                Inventory Tracker
              </h1>
              <p className="text-xs text-muted-foreground">
                Inventory management
              </p>
            </div>
          </div>
          <Button
            type="button"
            variant="ghost"
            size="icon"
            aria-label={isExpanded ? "Collapse sidebar" : "Expand sidebar"}
            aria-expanded={isExpanded}
            onClick={() => setIsExpanded((expanded) => !expanded)}
            className="shrink-0 text-slate-600 hover:text-slate-900"
          >
            {isExpanded ? (
              <PanelLeftClose aria-hidden="true" />
            ) : (
              <PanelLeftOpen aria-hidden="true" />
            )}
          </Button>
        </div>

        <nav
          aria-label="Primary navigation"
          className={`flex gap-2 overflow-x-auto pb-3 md:flex-col md:gap-6 md:pb-4 ${
            isExpanded ? "px-3 md:px-4" : "px-3 md:px-2"
          }`}
        >
          <section
            className={`flex shrink-0 gap-2 md:flex-col md:gap-1 ${
              isExpanded ? "" : "md:items-center"
            }`}
          >
            <p
              className={`hidden px-3 pb-1 text-xs font-semibold uppercase tracking-wide text-slate-400 md:block ${
                isExpanded ? "" : "md:hidden"
              }`}
            >
              Workspace
            </p>
            <NavLink
              to="/"
              end
              className={({ isActive }) =>
                `flex items-center rounded-lg px-3 py-2 text-sm font-medium transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring ${
                  isExpanded ? "gap-3" : "gap-0 md:justify-center md:px-2"
                } ${
                  isActive
                    ? "bg-slate-900 text-white"
                    : "text-slate-600 hover:bg-slate-100 hover:text-slate-900"
                }`
              }
              aria-label={isExpanded ? undefined : "Dashboard"}
              title={isExpanded ? undefined : "Dashboard"}
            >
              <LayoutDashboard className="h-4 w-4" aria-hidden="true" />
              <span className={isExpanded ? "" : "hidden"}>Dashboard</span>
            </NavLink>
          </section>

          {isAdmin && (
            <section
              className={`flex shrink-0 gap-2 md:flex-col md:gap-1 ${
                isExpanded ? "" : "md:items-center"
              }`}
            >
              <p
                className={`hidden px-3 pb-1 text-xs font-semibold uppercase tracking-wide text-slate-400 md:block ${
                  isExpanded ? "" : "md:hidden"
                }`}
              >
                Administration
              </p>
              <NavLink
                to="/admin/invitations"
                className={({ isActive }) =>
                  `flex items-center rounded-lg px-3 py-2 text-sm font-medium transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring ${
                    isExpanded ? "gap-3" : "gap-0 md:justify-center md:px-2"
                  } ${
                    isActive
                      ? "bg-slate-900 text-white"
                      : "text-slate-600 hover:bg-slate-100 hover:text-slate-900"
                  }`
                }
                aria-label={isExpanded ? undefined : "Invitations and access"}
                title={isExpanded ? undefined : "Invitations and access"}
              >
                <UserPlus className="h-4 w-4" aria-hidden="true" />
                <span className={isExpanded ? "" : "hidden"}>
                  Invitations &amp; access
                </span>
              </NavLink>
            </section>
          )}
        </nav>
      </aside>

      <div className={isExpanded ? "md:ml-64" : "md:ml-20"}>
        <Header />
        <main className="px-4 py-6 sm:px-6 lg:px-8">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default Layout;
