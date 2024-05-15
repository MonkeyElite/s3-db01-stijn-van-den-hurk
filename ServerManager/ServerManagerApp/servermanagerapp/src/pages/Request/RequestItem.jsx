import React from 'react';
import requestApi from '../../api/RequestApi';

const RequestItem = ({ request }) => {
    const deleteRequest = async (e) => {
        e.preventDefault();
        await requestApi.deleteRequest(request.id);
    };

    return (
        <div style={containerStyle}>
            <form onSubmit={deleteRequest}>
                <h3 style={titleStyle}>{request.title}</h3>
                <button type="submit" style={buttonStyle}>Delete</button>
            </form>
        </div>
    );
};

const containerStyle = {
  marginBottom: '20px',
};

const titleStyle = {
  marginBottom: '5px',
};

const buttonStyle = {
  backgroundColor: '#dc3545',
  color: 'white',
  border: 'none',
  padding: '10px 20px',
  borderRadius: '5px',
  cursor: 'pointer',
};

export default RequestItem;
