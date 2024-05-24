import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import requestApi from "../../api/RequestApi";

import NavBar from "../../components/navbar";

const RequestInfoPage = () => {
  const { id } = useParams();
  const [request, setRequest] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchRequest = async () => {
      try {
        const response = await requestApi.fetchRequestById(id);
        setRequest(response);
      } catch (error) {
        console.error("Error fetching request:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchRequest();
  }, [id]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (!request) {
    return <div>Request not found</div>;
  }

  return (
    <div className="p-4">
      <NavBar page="Request" />
      <div className="ml-[10%] max-w-[80%]">
        <h1 className="text-2xl text-white mb-4">Request Info</h1>
        <div className="bg-slate-700 p-4 rounded-md shadow-md">
          <h2 className="text-xl text-white">{request.title}</h2>
          <p className="text-white">{request.description}</p>
        </div>
      </div>
    </div>
  );
};

export default RequestInfoPage;
