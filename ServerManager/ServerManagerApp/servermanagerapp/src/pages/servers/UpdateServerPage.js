import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import serverApi from "../../api/ServerApi";

import UpdateServerForm from "../../components/Server/UpdateServerForm";
import NavBar from "../../components/navbar";

const UpdateServerPage = () => {
  const { id } = useParams();
  const [server, setServer] = useState(null);
  const [loading, setLoading] = useState(true);

  const { getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    const fetchServer = async () => {
      try {
        const token = await getAccessTokenSilently();
        const response = await serverApi.fetchServerById(id, token);
        setServer(response);
      } catch (error) {
        console.error("Error fetching server:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchServer();
  }, [id, getAccessTokenSilently]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (!server) {
    return <div>Server not found</div>;
  }

  return (
    <div>
      <NavBar page="Server" />
      <div className="flex flex-col items-center mt-8">
        <h2 className="text-3xl text-white">Update Server</h2>
        <div>
          <UpdateServerForm server={server} />
        </div>
      </div>
    </div>
  );
};

export default UpdateServerPage;
