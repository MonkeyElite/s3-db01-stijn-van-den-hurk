const baseUrl = `${process.env.REACT_APP_API_URL}/Session`;

const sessionApi = {
  fetchSessions: async (token) => {
    try {
      const response = await fetch(`${baseUrl}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        throw new Error("Failed to fetch sessions");
      }

      const sessionData = await response.json();
      return sessionData;
    } catch (error) {
      console.error("Error fetching sessions:", error);
      throw error;
    }
  },

  fetchSessionById: async (id, token) => {
    try {
      const response = await fetch(`${baseUrl}/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        throw new Error("Failed to fetch session");
      }

      const sessionData = await response.json();
      return sessionData;
    } catch (error) {
      console.error("Error fetching session:", error);
      throw error;
    }
  },

  postSession: async (session, token) => {
    try {
      const response = await fetch(baseUrl, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(session),
      });

      if (!response.ok) {
        throw new Error("Failed to create session");
      }

      const sessionData = await response.json();
      return sessionData;
    } catch (error) {
      console.error("Error creating session:", error);
      throw error;
    }
  },

  putSession: async (id, session, token) => {
    try {
      const response = await fetch(`${baseUrl}/${id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(session),
      });

      if (!response.ok) {
        throw new Error("Failed to update session");
      }

      const sessionData = await response.json();
      return sessionData;
    } catch (error) {
      console.error("Error updating session:", error);
      throw error;
    }
  },

  deleteSession: async (id, token) => {
    try {
      const response = await fetch(`${baseUrl}/${id}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        throw new Error("Failed to delete session");
      }
    } catch (error) {
      console.error("Error deleting session:", error);
      throw error;
    }
  },

  applyForSession: async (sessionId, userId, token) => {
    try {
      const response = await fetch(`${baseUrl}/${sessionId}/apply`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(userId),
      });

      if (!response.ok) {
        throw new Error("Failed to apply for session");
      }
    } catch (error) {
      console.error("Error applying for session:", error);
      throw error;
    }
  },

  unapplyFromSession: async (sessionId, userId, token) => {
    try {
      const response = await fetch(`${baseUrl}/${sessionId}/unapply`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(userId),
      });

      if (!response.ok) {
        throw new Error("Failed to unapply from session");
      }
    } catch (error) {
      console.error("Error unapplying from session:", error);
      throw error;
    }
  },

  fetchAppliedUsers: async (sessionId, token) => {
    try {
      const response = await fetch(`${baseUrl}/${sessionId}/users`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      });

      if (!response.ok) {
        throw new Error("Failed to fetch applied users");
      }

      const appliedUsers = await response.json();
      return appliedUsers;
    } catch (error) {
      console.error("Error fetching applied users:", error);
      throw error;
    }
  },
};

export default sessionApi;
