import React from "react";
import { Link } from "react-router-dom";
import { FaTrashAlt, FaPen } from "react-icons/fa";
import requestApi from "../api/RequestApi";

const RequestItem = ({ request, onDelete, onUpdate }) => {
  const deleteRequest = async (e) => {
    e.preventDefault();
    e.stopPropagation(); // Stop propagation to prevent Link click
    await requestApi.deleteRequest(request.id);
    onDelete(request.id);
  };

  const updateRequest = (e) => {
    e.preventDefault();
    e.stopPropagation(); // Stop propagation to prevent Link click
    onUpdate(request.id);
  };

  return (
    <Link to={`/request/info/${request.id}`} className="cursor-pointer">
      <div className="bg-slate-700 p-4 rounded-md shadow-md flex flex-col justify-between items-center mb-4 aspect-square">
        <h3 className="text-lg text-white">{request.title}</h3>
        <div className="flex items-center mt-2">
          <button
            onClick={updateRequest}
            className="bg-blue-500 text-white border-none px-4 py-2 rounded cursor-pointer mr-2"
          >
            <FaPen />
          </button>
          <button
            onClick={deleteRequest}
            className="bg-red-500 text-white border-none px-4 py-2 rounded cursor-pointer"
          >
            <FaTrashAlt />
          </button>
        </div>
      </div>
    </Link>
  );
};

export default RequestItem;
