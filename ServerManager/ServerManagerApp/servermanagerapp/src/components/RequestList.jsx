import React, { useState, useEffect } from "react";
import requestApi from "../api/RequestApi";
import RequestItem from "./RequestItem";

const RequestList = () => {
  const [requests, setRequests] = useState([]);

  useEffect(() => {
    async function fetchData() {
      try {
        const data = await requestApi.fetchRequests();
        setRequests(data);
      } catch (error) {
        console.error("Error fetching requests:", error);
      }
    }
    fetchData();
  }, []);

  const handleDelete = (id) => {
    setRequests(requests.filter((request) => request.id !== id));
  };

  const handleUpdate = (id) => {
    console.log(`Update request with ID ${id}`);
  };

  return (
    <div className="p-8">
      <h2 className="text-4xl text-white mb-6">Request List</h2>
      {requests && requests.length > 0 ? (
        <div className="grid gap-4 cols-4">
          {requests.map((request) => (
            <RequestItem
              key={request.id}
              request={request}
              onDelete={handleDelete}
              onUpdate={handleUpdate}
            />
          ))}
        </div>
      ) : (
        <p className="text-2xl text-white">No requests found.</p>
      )}
    </div>
  );
};

export default RequestList;
