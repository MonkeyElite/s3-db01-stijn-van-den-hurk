import React, { useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import sessionApi from "../../api/SessionApi";

const UpdateSessionForm = ({ session }) => {
  const [title, setTitle] = useState(session.title);
  const [description, setDescription] = useState(session.description);
  const [startTime, setStartTime] = useState(session.startTime);
  const [endTime, setEndTime] = useState(session.endTime);
  const [serverId, setServerId] = useState(session.serverId);
  const [errorMessage, setErrorMessage] = useState("");
  const { getAccessTokenSilently } = useAuth0();

  const validateForm = () => {
    if (!title.trim() || !description.trim() || !startTime || !endTime || !serverId) {
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

  const updateSession = async (e) => {
    e.preventDefault();

    if (!validateForm()) {
      return;
    }

    const updatedSession = { title, description, startTime, endTime, serverId };

    try {
      const token = await getAccessTokenSilently();
      await sessionApi.putSession(session.id, updatedSession, token);
      window.location.href = "/session";
    } catch (error) {
      console.error("Error updating session:", error);
      setErrorMessage("Failed to update session. Please try again later.");
    }
  };

  return (
    <div style={{ marginBottom: "20px" }}>
      <h2 className="text-white">Update Session</h2>
      {errorMessage && <p className="text-red-600">{errorMessage}</p>}
      <form onSubmit={updateSession} style={formStyle}>
        <input
          type="text"
          placeholder="Session Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          style={inputStyle}
          data-testid="update-session-title"
        />
        <textarea
          placeholder="Session Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          style={inputStyle}
          data-testid="update-session-description"
        />
        <input
          type="datetime-local"
          placeholder="Start Time"
          value={startTime}
          onChange={(e) => setStartTime(e.target.value)}
          style={inputStyle}
          data-testid="update-session-start-time"
        />
        <input
          type="datetime-local"
          placeholder="End Time"
          value={endTime}
          onChange={(e) => setEndTime(e.target.value)}
          style={inputStyle}
          data-testid="update-session-end-time"
        />
        <input
          type="text"
          placeholder="Server ID"
          value={serverId}
          onChange={(e) => setServerId(e.target.value)}
          style={inputStyle}
          data-testid="update-session-server-id"
        />
        <button
          className="text-white"
          type="submit"
          style={buttonStyle}
          data-testid="update-session-submit"
        >
          Submit
        </button>
      </form>
    </div>
  );
};

const formStyle = {
  marginBottom: "20px",
};

const inputStyle = {
  marginRight: "10px",
  padding: "10px",
  borderRadius: "5px",
  border: "1px solid #ccc",
};

const buttonStyle = {
  backgroundColor: "#007bff",
  color: "white",
  border: "none",
  padding: "10px 20px",
  borderRadius: "5px",
  cursor: "pointer",
  marginLeft: "10px",
};

export default UpdateSessionForm;
