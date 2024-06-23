import React from "react";
import { Link } from "react-router-dom";
import { FaTrashAlt, FaPen } from "react-icons/fa";
import { useAuth0 } from "@auth0/auth0-react";
import serverApi from "../..//api/ServerApi";

const ServerItem = ({ server, onDelete, onUpdate }) => {
  const { getAccessTokenSilently } = useAuth0();

  const deleteServer = async (e) => {
    e.preventDefault();
    e.stopPropagation();

    try {
      const token = await getAccessTokenSilently();
      await serverApi.deleteServer(server.id, token);
      onDelete(server.id);
    } catch (error) {
      console.error("Error deleting server:", error);
    }
  };

  const updateServer = (e) => {
    e.preventDefault();
    e.stopPropagation();
    onUpdate(server.id);
  };

  return (
    <Link to={`/server/info/${server.id}`} className="cursor-pointer">
      <div className="bg-slate-700 p-4 rounded-md shadow-md flex flex-col justify-between items-center mb-4 aspect-square">
        <h3 className="text-lg text-white">{server.title}</h3>
        <p className="text-white">{server.gameName}</p>
        <div className="flex items-center mt-2">
          <button
            onClick={updateServer}
            className="bg-blue-500 text-white border-none px-4 py-2 rounded cursor-pointer mr-2"
          >
            <FaPen />
          </button>
          <button
            onClick={deleteServer}
            className="bg-red-500 text-white border-none px-4 py-2 rounded cursor-pointer"
          >
            <FaTrashAlt />
          </button>
        </div>
      </div>
    </Link>
  );
};

export default ServerItem;
