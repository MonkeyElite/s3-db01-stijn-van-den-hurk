import React, { useState, useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import sessionApi from "../../api/SessionApi";
import SessionItem from "./SessionItem";

const SessionList = () => {
  const [sessions, setSessions] = useState([]);
  const { getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    async function fetchData() {
      try {
        const token = await getAccessTokenSilently();
        const data = await sessionApi.fetchSessions(token);
        setSessions(data);
      } catch (error) {
        console.error("Error fetching sessions:", error);
      }
    }
    fetchData();
  }, [getAccessTokenSilently]);

  const handleDelete = async (id) => {
    try {
      const token = await getAccessTokenSilently();
      await sessionApi.deleteSession(id, token);
      setSessions(sessions.filter((session) => session.id !== id));
    } catch (error) {
      console.error("Error deleting session:", error);
    }
  };

  const handleUpdate = (id) => {
    window.location.href = "/session/update/" + id;
  };

  return (
    <div className="p-8">
      <h2 className="text-4xl text-white mb-6">Session List</h2>
      <div className="overflow-y-auto">
        {sessions && sessions.length > 0 ? (
          <div className="grid gap-4 grid-cols-4">
            {sessions.map((session) => (
              <SessionItem
                key={session.id}
                session={session}
                onDelete={handleDelete}
                onUpdate={handleUpdate}
              />
            ))}
          </div>
        ) : (
          <p className="text-2xl text-white">No sessions found.</p>
        )}
      </div>
    </div>
  );
};

export default SessionList;
