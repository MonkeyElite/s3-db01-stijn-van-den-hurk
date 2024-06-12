import React, { useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import serverApi from "../../api/ServerApi";

const CreateServerForm = () => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [gameName, setGameName] = useState("");
  const [ip, setIp] = useState("");
  const [port, setPort] = useState("");
  const [password, setPassword] = useState("");
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

  const createServer = async (e) => {
    e.preventDefault();

    if (!validateForm()) {
      return;
    }

    const newServer = {
      Title: title.trim(),
      Description: description.trim(),
      GameName: gameName.trim(),
      Ip: ip.trim(),
      Port: parseInt(port.trim(), 10),
      Password: password.trim(),
    };

    try {
      const token = await getAccessTokenSilently();
      await serverApi.postServer(newServer, token);
      window.location.href = "/server";
    } catch (error) {
      console.error("Error creating server:", error);
      setErrorMessage("Failed to create server. Please try again later.");
    }
  };

  return (
    <div style={{ marginBottom: "20px" }}>
      {errorMessage && <p className="text-red-600">{errorMessage}</p>}
      <form onSubmit={createServer} style={formStyle}>
        <input
          type="text"
          placeholder="Server Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          style={inputStyle}
          data-testid="create-server-title"
        />
        <input
          type="text"
          placeholder="Server Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          style={inputStyle}
          data-testid="create-server-description"
        />
        <input
          type="text"
          placeholder="Game Name"
          value={gameName}
          onChange={(e) => setGameName(e.target.value)}
          style={inputStyle}
          data-testid="create-server-game-name"
        />
        <input
          type="text"
          placeholder="IP Address"
          value={ip}
          onChange={(e) => setIp(e.target.value)}
          style={inputStyle}
          data-testid="create-server-ip"
        />
        <input
          type="text"
          placeholder="Port"
          value={port}
          onChange={(e) => setPort(e.target.value)}
          style={inputStyle}
          data-testid="create-server-port"
        />
        <input
          type="text"
          placeholder="Password (optional)"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          style={inputStyle}
          data-testid="create-server-password"
        />
        <button
          className="text-white"
          type="submit"
          style={buttonStyle}
          data-testid="create-server-submit"
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

export default CreateServerForm;
