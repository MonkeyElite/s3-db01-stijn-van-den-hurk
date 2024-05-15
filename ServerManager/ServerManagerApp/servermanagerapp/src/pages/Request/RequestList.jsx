import React, { useState, useEffect } from "react";
import requestApi from "../../api/RequestApi";
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

  return (
    <div>
      <h2>Request List</h2>
      {requests && requests.length > 0 ? (
        <ul>
          {requests.map((request) => (
            <RequestItem key={request.id} request={request} />
          ))}
        </ul>
      ) : (
        <p>No requests found.</p>
      )}
    </div>
  );
};

export default RequestList;
