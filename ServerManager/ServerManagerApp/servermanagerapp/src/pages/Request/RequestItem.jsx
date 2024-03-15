import React from 'react';
import requestApi from '../../api/RequestApi';

const RequestItem = ({ request }) => {
    const deleteRequest = async (e) => {
        e.preventDefault();
        await requestApi.deleteRequest(request.id);
    };

    return (
        <div>
            <form onSubmit={deleteRequest}>
                <h3>{request.title}</h3>
                <button type="submit">Delete</button>
            </form>
        </div>
    );
};

export default RequestItem;
