import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import sessionApi from "../../api/SessionApi";
import UpdateSessionForm from "../../components/Sessions/UpdateSessionForm";
import NavBar from "../../components/navbar";

function UpdateSessionPage() {
  const { id } = useParams();
  const [session, setSession] = useState(null);
  const [loading, setLoading] = useState(true);
  const { getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    const fetchSession = async () => {
      try {
        const token = await getAccessTokenSilently();
        const response = await sessionApi.fetchSessionById(id, token);
        setSession(response);
      } catch (error) {
        console.error("Error fetching session:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchSession();
  }, [id]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (!session) {
    return <div>Session not found</div>;
  }

  return (
    <div>
      <NavBar page="Session" />
      <div className="flex flex-col items-center mt-8">
        <h2 className="text-3xl text-white">Update Session</h2>
        <div>
          <UpdateSessionForm session={session} />
        </div>
      </div>
    </div>
  );
}

export default UpdateSessionPage;
