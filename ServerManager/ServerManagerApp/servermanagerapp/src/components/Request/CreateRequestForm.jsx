import React, { useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import requestApi from "../../api/RequestApi";

const CreateRequestForm = () => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
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

  const createRequest = async (e) => {
    e.preventDefault();

    if (!validateForm()) {
      return;
    }

    const newRequest = {
      title: title.trim(),
      description: description.trim(),
    };

    try {
      const token = await getAccessTokenSilently();
      await requestApi.postRequest(newRequest, token);
      window.location.href = "/request";
    } catch (error) {
      console.error("Error creating request:", error);
      setErrorMessage("Failed to create request. Please try again later.");
    }
  };

  return (
    <div style={{ marginBottom: "20px" }}>
      {errorMessage && <p className="text-red-600">{errorMessage}</p>}
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
