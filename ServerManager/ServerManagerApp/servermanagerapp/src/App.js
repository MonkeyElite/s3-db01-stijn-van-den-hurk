import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import HomePage from "./pages/HomePage";

import RequestPage from "./pages/RequestPage";
import CreateRequestPage from "./pages/requests/CreateRequestPage";

import SessionsPage from './pages/SessionsPage';

function App() {
  return (
    <Router>
      <div className="App bg-gradient-to-bl from-slate-900 via-slate-700 to-slate-800 h-screen">
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/request" element={<RequestPage />} />
          <Route path="/request/create" element={<CreateRequestPage />} />
          <Route path="/sessions" element={<SessionsPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
