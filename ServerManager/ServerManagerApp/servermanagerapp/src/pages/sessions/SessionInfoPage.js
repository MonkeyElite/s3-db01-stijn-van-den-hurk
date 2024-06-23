import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import sessionApi from "../../api/SessionApi";
import NavBar from "../../components/navbar";

const SessionInfoPage = () => {
  const { id } = useParams();
  const [session, setSession] = useState(null);
  const [loading, setLoading] = useState(true);
  const [appliedUsers, setAppliedUsers] = useState([]);
  const { getAccessTokenSilently, user } = useAuth0();

  useEffect(() => {
    const fetchSession = async () => {
      try {
        const token = await getAccessTokenSilently();
        const response = await sessionApi.fetchSessionById(id, token);
        setSession(response);
        fetchAppliedUsers(id, token);
      } catch (error) {
        console.error("Error fetching session:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchSession();
  }, [id, getAccessTokenSilently]);

  const fetchAppliedUsers = async (id, token) => {
    try {
      const response = await sessionApi.fetchAppliedUsers(id, token);
      setAppliedUsers(response);
    } catch (error) {
      console.error("Error fetching applied users:", error);
    }
  };

  const handleApply = async () => {
    try {
      const token = await getAccessTokenSilently();
      await sessionApi.applyForSession(id, user.email, token);
      fetchAppliedUsers(id, token);
    } catch (error) {
      console.error("Error applying for session:", error);
    }
  };

  const handleUnapply = async () => {
    try {
      const token = await getAccessTokenSilently();
      await sessionApi.unapplyFromSession(id, user.email, token);
      fetchAppliedUsers(id, token);
    } catch (error) {
      console.error("Error unapplying from session:", error);
    }
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  if (!session) {
    return <div>Session not found</div>;
  }

  return (
    <div className="p-4">
      <NavBar page="Session" />
      <div className="ml-[10%] max-w-[80%]">
        <h1 className="text-2xl text-white mb-4">Session Info</h1>
        <div className="bg-slate-700 p-4 rounded-md shadow-md">
          <h2 className="text-xl text-white">{session.title}</h2>
          <p className="text-white">{session.description}</p>
          <p className="text-white">Start Time: {new Date(session.startTime).toLocaleString()}</p>
          <p className="text-white">End Time: {new Date(session.endTime).toLocaleString()}</p>
          <p className="text-white">Server ID: {session.serverId}</p>

          {user && (
            <div className="mt-4">
              {appliedUsers.some(u => u.email === user.email) ? (
                <button onClick={handleUnapply} className="bg-red-500 text-white px-3 py-1 rounded-md">
                  Unapply
                </button>
              ) : (
                <button onClick={handleApply} className="bg-green-500 text-white px-3 py-1 rounded-md">
                  Apply
                </button>
              )}
            </div>
          )}

          {appliedUsers.length > 0 && (
            <div className="mt-4">
              <h3 className="text-lg text-white">Users Applied:</h3>
              <ul className="text-white">
                {appliedUsers.map((user, index) => (
                  <li key={index}>{user.username}</li>
                ))}
              </ul>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default SessionInfoPage;
