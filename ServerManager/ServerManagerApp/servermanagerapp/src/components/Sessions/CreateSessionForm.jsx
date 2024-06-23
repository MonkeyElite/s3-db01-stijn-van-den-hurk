import React, { useState, useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import serverApi from "../../api/ServerApi";
import sessionApi from "../../api/SessionApi";

const CreateSessionForm = () => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");
  const [serverId, setServerId] = useState("");
  const [servers, setServers] = useState([]);
  const [errorMessage, setErrorMessage] = useState("");
  const { getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    const fetchServers = async () => {
      try {
        const token = await getAccessTokenSilently();
        const serverData = await serverApi.fetchServers(token);
        setServers(serverData);
      } catch (error) {
        console.error("Error fetching servers:", error);
      }
    };

    fetchServers();
  }, [getAccessTokenSilently]);

  const validateForm = () => {
    if (!title.trim() || !description.trim() || !startTime.trim() || !endTime.trim() || !serverId) {
      setErrorMessage("All fields are required.");
      return false;
    }
    if (title.length > 255) {
      setErrorMessage("Title must be less than or equal to 255 characters.");
      return false;
    }
    if (description.length > 10000) {
      setErrorMessage("Description must be less than or equal to 10000 characters.");
      return false;
    }
    return true;
  };

  const createSession = async (e) => {
    e.preventDefault();

    if (!validateForm()) {
      return;
    }

    const newSession = {
      title: title.trim(),
      description: description.trim(),
      startTime: new Date(startTime),
      endTime: new Date(endTime),
      serverId: parseInt(serverId, 10),
    };

    try {
      const token = await getAccessTokenSilently();
      await sessionApi.postSession(newSession, token);
      window.location.href = "/session";
    } catch (error) {
      console.error("Error creating session:", error);
      setErrorMessage("Failed to create session. Please try again later.");
    }
  };

  return (
    <div style={{ marginBottom: "20px" }}>
      {errorMessage && <p className="text-red-600">{errorMessage}</p>}
      <form onSubmit={createSession} style={formStyle}>
        <input
          type="text"
          placeholder="Session Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          style={inputStyle}
          data-testid="create-session-title"
        />
        <input
          type="text"
          placeholder="Session Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          style={inputStyle}
          data-testid="create-session-description"
        />
        <input
          type="datetime-local"
          placeholder="Start Time"
          value={startTime}
          onChange={(e) => setStartTime(e.target.value)}
          style={inputStyle}
          data-testid="create-session-start-time"
        />
        <input
          type="datetime-local"
          placeholder="End Time"
          value={endTime}
          onChange={(e) => setEndTime(e.target.value)}
          style={inputStyle}
          data-testid="create-session-end-time"
        />
        <select
          value={serverId}
          onChange={(e) => setServerId(e.target.value)}
          style={inputStyle}
          data-testid="create-session-server-id"
        >
          <option value="">Select Server</option>
          {servers.map((server) => (
            <option key={server.id} value={server.id}>
              {server.title}
            </option>
          ))}
        </select>
        <button
          className="text-white"
          type="submit"
          style={buttonStyle}
          data-testid="create-session-submit"
        >
          Submit
        </button>
      </form>
    </div>
  );
};

const formStyle = {
  display: "flex",
  flexDirection: "column",
  gap: "10px",
  maxWidth: "400px",
  margin: "0 auto",
};

const inputStyle = {
  padding: "10px",
  borderRadius: "5px",
  border: "1px solid #ccc",
  width: "100%",
  boxSizing: "border-box",
};

const buttonStyle = {
  backgroundColor: "#007bff",
  color: "white",
  border: "none",
  padding: "10px 20px",
  borderRadius: "5px",
  cursor: "pointer",
  alignSelf: "center",
  marginTop: "10px",
};

export default CreateSessionForm;
