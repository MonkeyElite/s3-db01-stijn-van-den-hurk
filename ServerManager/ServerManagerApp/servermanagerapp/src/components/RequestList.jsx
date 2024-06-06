import React, { useState, useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import requestApi from "../api/RequestApi";
import RequestItem from "./RequestItem";

const RequestList = () => {
  const [requests, setRequests] = useState([]);
  const { getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    async function fetchData() {
      try {
        const token = await getAccessTokenSilently();
        const data = await requestApi.fetchRequests(token);
        setRequests(data);
      } catch (error) {
        console.error("Error fetching requests:", error);
      }
    }
    fetchData();
  }, [getAccessTokenSilently]);

  const handleDelete = async (id) => {
    try {
      const token = await getAccessTokenSilently();
      await requestApi.deleteRequest(id, token);
      setRequests(requests.filter((request) => request.id !== id));
    } catch (error) {
      console.error("Error deleting request:", error);
    }
  };

  const handleUpdate = (id) => {
    window.location.href = "/request/update/" + id;
  };

  return (
    <div className="p-8">
      <h2 className="text-4xl text-white mb-6">Request List</h2>
      <div className="overflow-y-auto">
        {requests && requests.length > 0 ? (
          <div className="grid gap-4 grid-cols-4">
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
    </div>
  );
};

export default RequestList;
