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
    console.log(id);
    window.location.href = "/request/update/" + id;
  };

  return (
    <div className="p-8">
      <h2 className="text-4xl text-white mb-6">Request List</h2>
      <div className="overflow-y-auto">
        {requests && requests.length > 0 ? (
          <div className="grid gap-4 grid-cols-4">
            {requests.slice(0, 12).map((request) => (
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
    </div>
  );
};

export default RequestList;
