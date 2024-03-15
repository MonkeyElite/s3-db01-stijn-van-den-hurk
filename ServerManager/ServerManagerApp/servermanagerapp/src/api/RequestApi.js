const baseUrl = 'https://localhost:5001/api/Request';

const requestApi = {

    fetchRequests: async () => {
        try {
            const response = await fetch(`${baseUrl}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                new Error('Failed to fetch request');
            }

            const requestData = await response.json();

            console.log(requestData);
            return requestData;            
        } catch (error) {
            console.error('Error fetching request:', error);
            return error;
        }
    },

    fetchRequestById: async (id) => {
        try {
            const response = await fetch(`${baseUrl}/${id}`);
            if (!response.ok) {
                throw new Error('Failed to fetch request');
            }
            const requestData = await response.json();
            return requestData;
        } catch (error) {
            console.error('Error fetching request:', error);
            throw error;
        }
    },

    postRequest: async (request) => {
        try {
            const response = await fetch(baseUrl, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(request)
            });
            if (!response.ok) {
                throw new Error('Failed to create request');
            }
            const requestData = await response.json();
            return requestData;
        } catch (error) {
            console.error('Error creating request:', error);
            throw error;
        }
    },

    putRequest: async (id, request) => {
        try {
            console.log(id);
            console.log(request);
            const response = await fetch(`${baseUrl}/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(request)
            });
            if (!response.ok) {
                throw new Error('Failed to update request');
            }
            const requestData = await response.json();
            return requestData;
        } catch (error) {
            console.error('Error updating request:', error);
            throw error;
        }
    },

    deleteRequest: async (id) => {
        try {
            const response = await fetch(`${baseUrl}/${id}`, {
                method: 'DELETE'
            });
            if (!response.ok) {
                throw new Error('Failed to delete request');
            }
        } catch (error) {
            console.error('Error deleting request:', error);
            throw error;
        }
    }
};

export default requestApi;
