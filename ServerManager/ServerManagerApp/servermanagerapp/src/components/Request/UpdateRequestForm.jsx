import React, { useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import requestApi from "../../api/RequestApi";

const UpdateRequestForm = ({ request }) => {
  const [title, setTitle] = useState(request.title);
  const [description, setDescription] = useState(request.description);
  const [errorMessage, setErrorMessage] = useState("");
  const { getAccessTokenSilently } = useAuth0();

  const validateForm = () => {
    if (!title.trim() || !description.trim()) {
      setErrorMessage("Title and description are required.");
      return false;
    }
    if (title.length > 255) {
      setErrorMessage("Title must be less than or equal to 255 characters.");
      return false;
    }
    if (description.length > 10000) {
      setErrorMessage(
        "Description must be less than or equal to 10000 characters."
      );
      return false;
    }
    return true;
  };

  const updateRequest = async (e) => {
    e.preventDefault();

    if (!validateForm()) {
      return;
    }

    const updatedRequest = { title, description };

    try {
      const token = await getAccessTokenSilently();
      await requestApi.putRequest(request.id, updatedRequest, token);
      window.location.href = "/request";
    } catch (error) {
      console.error("Error updating request:", error);
      setErrorMessage("Failed to update request. Please try again later.");
    }
  };

  return (
    <div style={{ marginBottom: "20px" }}>
      <h2 className="text-white">Update Request</h2>
      {errorMessage && <p className="text-red-600">{errorMessage}</p>}
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
