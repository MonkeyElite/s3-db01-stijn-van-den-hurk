import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import requestApi from "../../api/RequestApi";

import RequestForm from "../../components/UpdateRequestForm";
import NavBar from "../../components/navbar";

function UpdateRequestPage() {
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
    <div>
      <NavBar page="Request" />
      <div class="flex flex-col items-center mt-8">
        <h2 class="text-3xl text-white">Update Request</h2>
        <div>
          <RequestForm request={request} />
        </div>
      </div>
    </div>
  );
}

export default UpdateRequestPage;
