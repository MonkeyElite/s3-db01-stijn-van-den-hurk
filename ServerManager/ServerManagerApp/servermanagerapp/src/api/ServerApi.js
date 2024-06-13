const baseUrl = "https://localhost:5001/api/Server";

const serverApi = {
  fetchServers: async (token) => {
    try {
      const response = await fetch(`${baseUrl}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        throw new Error("Failed to fetch servers");
      }

      const serverData = await response.json();
      return serverData;
    } catch (error) {
      console.error("Error fetching servers:", error);
      throw error;
    }
  },

  fetchServerById: async (id, token) => {
    try {
      console.log("Winfajsndjs")
      const response = await fetch(`${baseUrl}/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        throw new Error("Failed to fetch server");
      }

      const serverData = await response.json();
      return serverData;
    } catch (error) {
      console.error("Error fetching server:", error);
      throw error;
    }
  },

  postServer: async (server, token) => {
    try {
      const response = await fetch(baseUrl, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(server),
      });

      if (!response.ok) {
        throw new Error("Failed to create server");
      }

      const serverData = await response.json();
      return serverData;
    } catch (error) {
      console.error("Error creating server:", error);
      throw error;
    }
  },

  putServer: async (id, server, token) => {
    try {
      const response = await fetch(`${baseUrl}/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(server),
      });

      if (!response.ok) {
        throw new Error("Failed to update server");
      }

      const serverData = await response.json();
      return serverData;
    } catch (error) {
      console.error("Error updating server:", error);
      throw error;
    }
  },

  deleteServer: async (id, token) => {
    try {
      const response = await fetch(`${baseUrl}/${id}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        throw new Error("Failed to delete server");
      }
    } catch (error) {
      console.error("Error deleting server:", error);
      throw error;
    }
  },
};

export default serverApi;
