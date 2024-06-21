import React from "react";
import { Link } from "react-router-dom";
import { FaTrashAlt, FaPen } from "react-icons/fa";
import { useAuth0 } from "@auth0/auth0-react";
import sessionApi from "../../api/SessionApi";

const SessionItem = ({ session, onDelete, onUpdate }) => {
  const { getAccessTokenSilently } = useAuth0();

  const deleteSession = async (e) => {
    e.preventDefault();
    e.stopPropagation();

    try {
      const token = await getAccessTokenSilently();
      await sessionApi.deleteSession(session.id, token);
      onDelete(session.id);
    } catch (error) {
      console.error("Error deleting session:", error);
    }
  };

  const updateSession = (e) => {
    e.preventDefault();
    e.stopPropagation();
    onUpdate(session.id);
  };

  return (
    <Link to={`/session/info/${session.id}`} className="cursor-pointer">
      <div className="bg-slate-700 p-4 rounded-md shadow-md flex flex-col justify-between items-center mb-4 aspect-square">
        <h3 className="text-lg text-white">{session.title}</h3>
        <div className="flex items-center mt-2">
          <button
            onClick={updateSession}
            className="bg-blue-500 text-white border-none px-4 py-2 rounded cursor-pointer mr-2"
          >
            <FaPen />
          </button>
          <button
            onClick={deleteSession}
            className="bg-red-500 text-white border-none px-4 py-2 rounded cursor-pointer"
          >
            <FaTrashAlt />
          </button>
        </div>
      </div>
    </Link>
  );
};

export default SessionItem;
