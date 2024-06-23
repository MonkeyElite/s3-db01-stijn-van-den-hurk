import React, { useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import serverApi from "../../api/ServerApi";

const UpdateServerForm = ({ server }) => {
  const [title, setTitle] = useState(server.title);
  const [description, setDescription] = useState(server.description);
  const [gameName, setGameName] = useState(server.gameName);
  const [ip, setIp] = useState(server.ip);
  const [port, setPort] = useState(server.port);
  const [password, setPassword] = useState(server.password);
  const [errorMessage, setErrorMessage] = useState("");
  const { getAccessTokenSilently } = useAuth0();

  const validateForm = () => {
    if (!title.trim() || !description.trim() || !gameName.trim() || !ip.trim() || !port.trim()) {
      setErrorMessage("All fields except password are required.");
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

  const updateServer = async (e) => {
    e.preventDefault();

    if (!validateForm()) {
      return;
    }

    const updatedServer = {
      title: title.trim(),
      description: description.trim(),
      gameName: gameName.trim(),
      ip: ip.trim(),
      port: parseInt(port.trim(), 10),
      password: password.trim(),
    };

    try {
      const token = await getAccessTokenSilently();
      await serverApi.putServer(server.id, updatedServer, token);
      window.location.href = `/server/info/${server.id}`;
    } catch (error) {
      console.error("Error updating server:", error);
      setErrorMessage("Failed to update server. Please try again later.");
    }
  };

  return (
    <div style={{ marginBottom: "20px" }}>
      {errorMessage && <p className="text-red-600">{errorMessage}</p>}
      <form onSubmit={updateServer} style={formStyle}>
        <input
          type="text"
          placeholder="Server Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          style={inputStyle}
          data-testid="update-server-title"
        />
        <input
          type="text"
          placeholder="Server Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          style={inputStyle}
          data-testid="update-server-description"
        />
        <input
          type="text"
          placeholder="Game Name"
          value={gameName}
          onChange={(e) => setGameName(e.target.value)}
          style={inputStyle}
          data-testid="update-server-game-name"
        />
        <input
          type="text"
          placeholder="IP Address"
          value={ip}
          onChange={(e) => setIp(e.target.value)}
          style={inputStyle}
          data-testid="update-server-ip"
        />
        <input
          type="text"
          placeholder="Port"
          value={port}
          onChange={(e) => setPort(e.target.value)}
          style={inputStyle}
          data-testid="update-server-port"
        />
        <input
          type="password"
          placeholder="Password (optional)"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          style={inputStyle}
          data-testid="update-server-password"
        />
        <button
          className="text-white"
          type="submit"
          style={buttonStyle}
          data-testid="update-server-submit"
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

export default UpdateServerForm;
