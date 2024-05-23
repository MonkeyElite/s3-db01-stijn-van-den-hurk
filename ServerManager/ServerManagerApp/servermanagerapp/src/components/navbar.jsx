import React from "react";
import { Link } from "react-router-dom";

const NavBar = ({ page }) => {
  return (
    <nav className="bg-transparent mb-5 sticky top-0">
      <div className="mx-auto max-w-7xl px-2 sm:px-6 lg:px-8">
        <div className="relative flex h-16 items-center justify-between">
          <div className="flex flex-1 items-center justify-center">
            <div className="flex space-x-4 mt-4 mb-4">
              <Link
                to="/"
                className={`${
                  page === "Home"
                    ? "bg-gray-900 text-white"
                    : "text-slate-500 hover:bg-slate-800 hover:text-white"
                } rounded-md px-3 py-2 text-sm font-medium`}
                aria-current={page === "Home" ? "page" : undefined}
              >
                Home
              </Link>
              <Link
                to="/request"
                className={`${
                  page === "Request"
                    ? "bg-gray-900 text-white"
                    : "text-slate-500 hover:bg-slate-800 hover:text-white"
                } rounded-md px-3 py-2 text-sm font-medium`}
                aria-current={page === "Request" ? "page" : undefined}
              >
                Request
              </Link>
              <Link
                to="/sessions"
                className={`${
                  page === "Sessions"
                    ? "bg-gray-900 text-white"
                    : "text-slate-500 hover:bg-slate-800 hover:text-white"
                } rounded-md px-3 py-2 text-sm font-medium`}
                aria-current={page === "Sessions" ? "page" : undefined}
              >
                Sessions
              </Link>
            </div>
          </div>
        </div>
      </div>
    </nav>
  );
};

export default NavBar;
