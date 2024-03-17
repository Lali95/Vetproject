import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import MedicalRecordCard from '../Components/MedicalRecordCard';

const DisplayMedicalRecord = () => {
  const { id } = useParams();
  const [medicalRecord, setMedicalRecord] = useState(null);

  useEffect(() => {
    // Fetch medical record details based on ID from API
    fetch(`/api/MedicalRecord/${id}`)
      .then(response => {
        if (!response.ok) {
          throw new Error('Failed to fetch medical record');
        }
        return response.json();
      })
      .then(data => {
        setMedicalRecord(data);
      })
      .catch(error => {
        console.error('Error fetching medical record:', error);
      });
  }, [id]);

  return (
    <div>
      <h1>Medical Record Details</h1>
      {medicalRecord ? (
        <div>
          {/* Render the MedicalRecordCard component with the medicalRecord data */}
          <MedicalRecordCard medicalRecord={medicalRecord} />
        </div>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
};

export default DisplayMedicalRecord;
