import React from "react";
import "./CalculateScore.css";

export function CalculateScore({ Name, School, total, goal }) {
  const percent = ((total / goal) * 100).toFixed(2) + "%";

  return (
    <div className="formatstyle">
      <h1 style={{ color: "brown" }}>Student Details:</h1>
      <div className="Name">Name: {Name}</div>
      <div className="School">School: {School}</div>
      <div className="Total">Total: {total} Marks</div>
      <div className="Score">Score: {percent}</div>
    </div>
  );
}
