import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import serverApi from "../../api/ServerApi";

import NavBar from "../../components/navbar";

const ServerInfoPage = () => {
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
    <div className="p-4">
      <NavBar page="Server" />
      <div className="ml-[10%] max-w-[80%]">
        <h1 className="text-2xl text-white mb-4">Server Info</h1>
        <div className="bg-slate-700 p-4 rounded-md shadow-md">
          <h2 className="text-xl text-white">{server.title}</h2>
          <p className="text-white">{server.description}</p>
          <p className="text-white"><strong>Game Name:</strong> {server.gameName}</p>
          <p className="text-white"><strong>IP:</strong> {server.ip}</p>
          <p className="text-white"><strong>Port:</strong> {server.port}</p>
          <p className="text-white"><strong>Password:</strong> {server.password ? server.password : 'No password set'}</p>
        </div>
      </div>
    </div>
  );
};

export default ServerInfoPage;
