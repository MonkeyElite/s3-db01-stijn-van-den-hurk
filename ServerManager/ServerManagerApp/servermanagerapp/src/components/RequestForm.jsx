import React, { useState } from "react";
import requestApi from "../api/RequestApi";

const RequestForm = () => {
  const [id, setId] = useState("");
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  const createRequest = async (e) => {
    e.preventDefault();
    const newRequest = { title, description };
    await requestApi.postRequest(newRequest);
    setTitle("");
    setDescription("");
  };

  const updateRequest = async (e) => {
    e.preventDefault();
    const newRequest = { title, description };
    await requestApi.putRequest(id, newRequest);
    setId("");
    setTitle("");
    setDescription("");
  };

  return (
    <div style={{ marginBottom: "20px" }}>
      <h2 class="text-white">Create New Request</h2>
      <form onSubmit={createRequest} style={formStyle}>
        <input
          type="text"
          placeholder="Request Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          style={inputStyle}
        />
        <input
          type="text"
          placeholder="Request Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          style={inputStyle}
        />
        <button class="text-white" type="submit" style={buttonStyle}>
          Submit
        </button>
      </form>

      <h2 class="text-white">Update Request</h2>
      <form onSubmit={updateRequest} style={formStyle}>
        <input
          type="number"
          placeholder="Request Id"
          value={id}
          onChange={(e) => setId(e.target.value)}
          style={inputStyle}
        />
        <input
          type="text"
          placeholder="Request Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          style={inputStyle}
        />
        <input
          type="text"
          placeholder="Request Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          style={inputStyle}
        />
        <button class="text-white" type="submit" style={buttonStyle}>
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

export default RequestForm;
