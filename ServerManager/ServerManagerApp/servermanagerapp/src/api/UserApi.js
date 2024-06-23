const baseUrl = `${process.env.REACT_APP_API_URL}/User`;

const userApi = {
  registerUser: async (user) => {
    try {
      const response = await fetch(`${baseUrl}/register`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(user),
      });

      if (!response.ok) {
        throw new Error("Failed to register user");
      }

      const userData = await response.json();
      return userData;
    } catch (error) {
      console.error("Error registering user:", error);
      throw error;
    }
  },
};

export default userApi;
