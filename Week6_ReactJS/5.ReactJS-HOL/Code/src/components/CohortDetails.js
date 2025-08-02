import React from 'react';
import styles from './CohortDetails.module.css';

const CohortDetails = ({ cohort }) => {
  return (
    <div className={styles.box}>
      <h3>{cohort.name}</h3>
      <dl>
        <dt>Started On</dt>
        <dd>{cohort.startedOn}</dd>
        
        <dt>Current Status</dt>
        <dd>
          <span className={cohort.currentStatus === 'Ongoing' ? styles.ongoing : styles.other}>
            {cohort.currentStatus}
          </span>
        </dd>
        
        <dt>Coach</dt>
        <dd>{cohort.coach}</dd>
        
        <dt>Trainer</dt>
        <dd>{cohort.trainer}</dd>
      </dl>
    </div>
  );
};

export default CohortDetails;