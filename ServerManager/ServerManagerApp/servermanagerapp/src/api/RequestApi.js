const baseUrl = `${process.env.REACT_APP_API_URL}/Request`;

const requestApi = {
  fetchRequests: async (token) => {
    try {
      const response = await fetch(`${baseUrl}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        throw new Error("Failed to fetch request");
      }

      const requestData = await response.json();
      return requestData;
    } catch (error) {
      console.error("Error fetching request:", error);
      throw error;
    }
  },

  fetchRequestById: async (id, token) => {
    try {
      const response = await fetch(`${baseUrl}/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        throw new Error("Failed to fetch request");
      }

      const requestData = await response.json();
      return requestData;
    } catch (error) {
      console.error("Error fetching request:", error);
      throw error;
    }
  },

  postRequest: async (request, token) => {
    try {
      const response = await fetch(baseUrl, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(request),
      });

      if (!response.ok) {
        throw new Error("Failed to create request");
      }

      const requestData = await response.json();
      return requestData;
    } catch (error) {
      console.error("Error creating request:", error);
      throw error;
    }
  },

  putRequest: async (id, request, token) => {
    try {
      const response = await fetch(`${baseUrl}/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(request),
      });

      if (!response.ok) {
        throw new Error("Failed to update request");
      }

      const requestData = await response.json();
      return requestData;
    } catch (error) {
      console.error("Error updating request:", error);
      throw error;
    }
  },

  deleteRequest: async (id, token) => {
    try {
      const response = await fetch(`${baseUrl}/${id}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        throw new Error("Failed to delete request");
      }
    } catch (error) {
      console.error("Error deleting request:", error);
      throw error;
    }
  },
};

export default requestApi;
