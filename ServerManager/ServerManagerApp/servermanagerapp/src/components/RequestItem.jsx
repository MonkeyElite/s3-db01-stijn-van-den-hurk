import React from "react";
import { FaTrashAlt, FaPen } from "react-icons/fa";
import requestApi from "../api/RequestApi";

const RequestItem = ({ request, onDelete, onUpdate }) => {
  const deleteRequest = async (e) => {
    e.preventDefault();
    await requestApi.deleteRequest(request.id);
    onDelete(request.id);
  };

  const updateRequest = () => {
    onUpdate(request.id);
  };

  return (
    <div className="bg-slate-700 p-4 rounded-md shadow-md flex justify-between items-center mb-4">
      <h3 className="text-lg text-white">{request.title}</h3>
      <div className="flex items-center">
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
  );
};

export default RequestItem;
