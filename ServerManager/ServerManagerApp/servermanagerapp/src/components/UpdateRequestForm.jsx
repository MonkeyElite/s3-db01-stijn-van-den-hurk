import React, { useState } from "react";
import requestApi from "../api/RequestApi";

const UpdateRequestForm = ({ request }) => {
  const [title, setTitle] = useState(request.title);
  const [description, setDescription] = useState(request.description);

  const updateRequest = async (e) => {
    e.preventDefault();
    const newRequest = { title, description };
    await requestApi.putRequest(request.id, newRequest);
    window.location.href = "/request";
  };

  return (
    <div style={{ marginBottom: "20px" }}>
      <h2 className="text-white">Update Request</h2>
      <form onSubmit={updateRequest} style={formStyle}>
        <input
          type="text"
          placeholder="Request Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          style={inputStyle}
          data-testid="update-request-title"
        />
        <input
          type="text"
          placeholder="Request Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          style={inputStyle}
          data-testid="update-request-description"
        />
        <button
          className="text-white"
          type="submit"
          style={buttonStyle}
          data-testid="update-request-submit"
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

export default UpdateRequestForm;
