import React, { useState } from "react";
import requestApi from "../api/RequestApi";

const CreateRequestForm = () => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  const createRequest = async (e) => {
    e.preventDefault();
    const newRequest = { title, description };
    await requestApi.postRequest(newRequest);
    setTitle("");
    setDescription("");
    window.location.href = "/request";
  };

  return (
    <div style={{ marginBottom: "20px" }}>
      <h2 className="text-white">Create New Request</h2>
      <form onSubmit={createRequest} style={formStyle}>
        <input
          type="text"
          placeholder="Request Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          style={inputStyle}
          data-testid="create-request-title"
        />
        <input
          type="text"
          placeholder="Request Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          style={inputStyle}
          data-testid="create-request-description"
        />
        <button
          className="text-white"
          type="submit"
          style={buttonStyle}
          data-testid="create-request-submit"
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

export default CreateRequestForm;
