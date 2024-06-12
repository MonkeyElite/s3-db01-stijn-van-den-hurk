import React, { useState, useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import serverApi from "../../api/ServerApi";
import ServerItem from "./ServerItem";

const ServerList = () => {
  const [servers, setServers] = useState([]);
  const { getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    async function fetchData() {
      try {
        const token = await getAccessTokenSilently();
        const data = await serverApi.fetchServers(token);
        setServers(data);
      } catch (error) {
        console.error("Error fetching servers:", error);
      }
    }
    fetchData();
  }, [getAccessTokenSilently]);

  const handleDelete = async (id) => {
    try {
      const token = await getAccessTokenSilently();
      await serverApi.deleteServer(id, token);
      setServers(servers.filter((server) => server.id !== id));
    } catch (error) {
      console.error("Error deleting server:", error);
    }
  };

  const handleUpdate = (id) => {
    window.location.href = "/server/update/" + id;
  };

  return (
    <div className="p-8">
      <h2 className="text-4xl text-white mb-6">Server List</h2>
      <div className="overflow-y-auto">
        {servers && servers.length > 0 ? (
          <div className="grid gap-4 grid-cols-4">
            {servers.map((server) => (
              <ServerItem
                key={server.id}
                server={server}
                onDelete={handleDelete}
                onUpdate={handleUpdate}
              />
            ))}
          </div>
        ) : (
          <p className="text-2xl text-white">No servers found.</p>
        )}
      </div>
    </div>
  );
};

export default ServerList;
