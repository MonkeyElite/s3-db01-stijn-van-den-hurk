import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import requestApi from "../../api/RequestApi";

import RequestForm from "../../components/Request/UpdateRequestForm";
import NavBar from "../../components/navbar";

function UpdateRequestPage() {
  const { id } = useParams();
  const [request, setRequest] = useState(null);
  const [loading, setLoading] = useState(true);

  const { getAccessTokenSilently } = useAuth0();
  
  useEffect(() => {
    const fetchRequest = async () => {
      try {
        const token = await getAccessTokenSilently();
        const response = await requestApi.fetchRequestById(id, token);
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
