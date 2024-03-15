import React, { useState } from 'react';
import requestApi from '../../api/RequestApi';

const RequestForm = () => {
    const [id, setId] = useState('');
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');

    const createRequest = async (e) => {
        e.preventDefault();
        const newRequest = { title, description };
        await requestApi.postRequest(newRequest);
        setTitle('');
        setDescription('');
    };

    const updateRequest = async (e) => {
        e.preventDefault();
        const newRequest = { title, description };
        await requestApi.putRequest(id, newRequest);
        setId('');
        setTitle('');
        setDescription('');
    };

    return (
        <div>
            <h2>Create New Request</h2>
            <form onSubmit={createRequest}>
                <input type="text" placeholder="Request Title" value={title} onChange={(e) => setTitle(e.target.value)} />
                <input type="text" placeholder="Request Description" value={description} onChange={(e) => setDescription(e.target.value)} />
                <button type="submit">Submit</button>
            </form>

            <h2>Update Request</h2>
            <form onSubmit={updateRequest}>
                <input type="number" placeholder="Request Id" value={id} onChange={(e) => setId(e.target.value)} />
                <input type="text" placeholder="Request Title" value={title} onChange={(e) => setTitle(e.target.value)} />
                <input type="text" placeholder="Request Description" value={description} onChange={(e) => setDescription(e.target.value)} />
                <button type="submit">Submit</button>
            </form>
        </div>
    );
};

export default RequestForm;