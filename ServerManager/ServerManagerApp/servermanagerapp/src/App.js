import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

import HomePage from "./pages/HomePage";

import RequestPage from "./pages/RequestPage";
import CreateRequestPage from "./pages/requests/CreateRequestPage";
import UpdateRequestPage from "./pages/requests/UpdateRequestPage";
import RequestInfoPage from "./pages/requests/UpdateRequestPage";

import SessionsPage from "./pages/SessionsPage";

import ServersPage from "./pages/ServersPage";
import CreateServerPage from "./pages/servers/CreateServerPage";
import UpdateServerPage from "./pages/servers/UpdateServerPage";
import ServerInfoPage from "./pages/servers/ServerInfoPage";

function App() {
  return (
    <Router>
      <div className="App bg-gradient-to-bl from-slate-900 via-slate-700 to-slate-800 min-h-screen">
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/request" element={<RequestPage />} />
          <Route path="/request/create" element={<CreateRequestPage />} />
          <Route path="/request/update/:id" element={<UpdateRequestPage />} />
          <Route path="/request/info/:id" element={<RequestInfoPage />} />
          <Route path="/session" element={<SessionsPage />} />
          <Route path="/server" element={<ServersPage />} />
          <Route path="/server/create" element={<CreateServerPage />} />
          <Route path="/server/update/:id" element={<UpdateServerPage />} />
          <Route path="/server/info/:id" element={<ServerInfoPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
