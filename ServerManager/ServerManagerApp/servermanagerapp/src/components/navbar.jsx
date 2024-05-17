import React from "react";

const NavBar = ({ request }) => {
  return (
    <nav class="bg-slate-900">
      <div class="mx-auto max-w-7xl px-2 sm:px-6 lg:px-8">
        <div class="relative flex h-16 items-center justify-between">
          <div class="flex flex-1 items-center justify-center">
            <div class="flex space-x-4">
              <a
                href="#"
                class="bg-gray-800 text-white rounded-md px-3 py-2 text-sm font-medium"
                aria-current="page"
              >
                Dashboard
              </a>
              <a
                href="#"
                class="text-slate-500 hover:bg-slate-800 hover:text-white rounded-md px-3 py-2 text-sm font-medium"
              >
                Team
              </a>
              <a
                href="#"
                class="text-slate-500 hover:bg-slate-800 hover:text-white rounded-md px-3 py-2 text-sm font-medium"
              >
                Projects
              </a>
              <a
                href="#"
                class="text-slate-500 hover:bg-slate-800 hover:text-white rounded-md px-3 py-2 text-sm font-medium"
              >
                Calendar
              </a>
            </div>
          </div>
        </div>
      </div>
    </nav>
  );
};

export default NavBar;
