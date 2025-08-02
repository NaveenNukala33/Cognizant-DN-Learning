import React from 'react';
import { Link } from 'react-router-dom';
import trainersMock from '../data/TrainersMock'; // Verify this import path

function TrainersList() {
  return (
    <div>
      <h1>Trainers Details</h1>
      <ul>
        {trainersMock.map(trainer => (
          <li key={trainer.trainerId}>
            <Link to={`/trainers/${trainer.trainerId}`}>
              {trainer.name} ({trainer.technology})
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default TrainersList;