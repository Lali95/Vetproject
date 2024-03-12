import React from 'react';
import { Link } from 'react-router-dom';
import '../Css/medicalRecords.css'; // Import CSS file for styling

const MedicalRecords = () => {
  return (
    <div>
      <h1>Medical Records</h1>
      <Link to="/search-medical-record" className="modern-button">Search Medical Record</Link>
      <Link to="/create-medical-record" className="modern-button">Create New Medical Record</Link>
    </div>
  );
};

export default MedicalRecords;
