import React from "react";
import requestApi from "../api/RequestApi";

const RequestItem = ({ request }) => {
  const deleteRequest = async (e) => {
    e.preventDefault();
    await requestApi.deleteRequest(request.id);
  };

  return (
    <div>
      <form className="flex items-center mt-5" onSubmit={deleteRequest}>
        <h3 className="mr-2 text-white">{request.title}</h3>
        <button
          type="submit"
          className="bg-red-500 text-white border-none px-4 py-2 rounded cursor-pointer"
        >
          Delete
        </button>
      </form>
    </div>
  );
};

export default RequestItem;
