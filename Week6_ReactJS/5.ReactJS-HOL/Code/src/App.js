import React from 'react';
import CohortDetails from './components/CohortDetails';

const cohorts = [
  {
    name: "INTADMDF10 - .NET FSD",
    startedOn: "22-Feb-2022",
    currentStatus: "Scheduled",
    coach: "Asthma",
    trainer: "Jojo Jose"
  },
  {
    name: "ADM21JF014 - Java FSD",
    startedOn: "10-Sep-2021",
    currentStatus: "Ongoing",
    coach: "Apoorv",
    trainer: "Elisa Smith"
  },
  {
    name: "CDBJF21025 - Java FSD",
    startedOn: "24-Dec-2021",
    currentStatus: "Ongoing",
    coach: "Asthma",
    trainer: "John Doe"
  }
];

function App() {
  return (
    <div className="App">
      <h1>Cohorts Details</h1>
      {cohorts.map((cohort, index) => (
        <CohortDetails key={index} cohort={cohort} />
      ))}
    </div>
  );
}

export default App;