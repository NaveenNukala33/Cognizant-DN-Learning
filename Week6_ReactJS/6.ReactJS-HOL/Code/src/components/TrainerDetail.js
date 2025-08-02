import React from 'react';
import { useParams } from 'react-router-dom';

function TrainerDetail() {
    const { id } = useParams();
    return (
        <div>
            <h1>Trainer Details</h1>
            <p>ID: {id}</p>
        </div>
    );
}

export default TrainerDetail;
