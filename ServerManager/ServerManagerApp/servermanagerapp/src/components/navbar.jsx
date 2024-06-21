import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import userApi from "../api/UserApi";

const NavBar = ({ page }) => {
  const { loginWithRedirect, logout, isAuthenticated, user } = useAuth0();
  const [username, setUsername] = useState(null);

  useEffect(() => {
    const fetchUsername = async () => {
      if (isAuthenticated) {
        const fetchedUsername = user?.username ?? user?.nickname ?? user?.name ?? null;
        setUsername(fetchedUsername);
      }
    };

    fetchUsername();
  }, [isAuthenticated, user]);

  useEffect(() => {
    if (isAuthenticated) {
      const registerUser = async () => {
        try {
          await userApi.registerUser({ username: (user?.username || user?.name), email: user?.email, sub: user?.sub });
          console.log("User created successfully");
        } catch (error) {
          console.error("Error creating user:", error);
        }
      };
      registerUser();
    }
  }, [isAuthenticated, user]);

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
                to="/session"
                className={`${
                  page === "Session"
                    ? "bg-gray-900 text-white"
                    : "text-slate-500 hover:bg-slate-800 hover:text-white"
                } rounded-md px-3 py-2 text-sm font-medium`}
                aria-current={page === "Session" ? "page" : undefined}
              >
                Sessions
              </Link>
              <Link
                to="/server"
                className={`${
                  page === "Server"
                    ? "bg-gray-900 text-white"
                    : "text-slate-500 hover:bg-slate-800 hover:text-white"
                } rounded-md px-3 py-2 text-sm font-medium`}
                aria-current={page === "Server" ? "page" : undefined}
              >
                Servers
              </Link>
              {isAuthenticated && (
                <p className="text-slate-500 rounded-md px-3 py-2 text-sm font-medium">Welcome {username}</p>
              )}
              {isAuthenticated ? (
                <button onClick={() => logout({ returnTo: window.location.origin })} className="text-slate-500 hover:bg-slate-800 hover:text-white rounded-md px-3 py-2 text-sm font-medium">
                  Logout
                </button>
              ) : (
                <>
                  <button
                    onClick={() => loginWithRedirect()}
                    className="text-slate-500 hover:bg-slate-800 hover:text-white rounded-md px-3 py-2 text-sm font-medium"
                  >
                    Login
                  </button>
                  <button
                    onClick={() => loginWithRedirect({ screen_hint: "signup" })}
                    className="text-slate-500 hover:bg-slate-800 hover:text-white rounded-md px-3 py-2 text-sm font-medium"
                  >
                    Register
                  </button>
                </>
              )}
            </div>
          </div>
        </div>
      </div>
    </nav>
  );
};

export default NavBar;
