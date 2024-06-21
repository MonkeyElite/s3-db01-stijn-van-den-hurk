import React from "react";
import SessionList from "../components/Sessions/SessionList";
import NavBar from "../components/navbar";
import { Link } from "react-router-dom";

function SessionPage() {
  return (
    <div>
      <NavBar page="Session" />
      <div className="flex justify-center">
        <div>
          <SessionList />
        </div>
      </div>
      <div className="flex justify-center">
        <Link
          to="/session/create"
          className="bg-blue-500 text-white border-none px-5 py-2.5 rounded cursor-pointer ml-2.5 mb-5"
          data-testid="create-session-button"
        >
          Create new session
        </Link>
      </div>
    </div>
  );
}

export default SessionPage;
