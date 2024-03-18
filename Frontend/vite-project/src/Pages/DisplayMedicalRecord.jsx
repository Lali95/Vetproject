import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import MedicalRecordCard from '../Components/MedicalRecordCard';

const DisplayMedicalRecord = () => {
  const { id } = useParams();

  const [medicalRecord, setMedicalRecord] = useState(null);
  const [showMessage, setShowMessage] = useState(false);
  const [deleteComplete, setDeleteComplete] = useState(false);

  useEffect(() => {
   
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

  const handleDelete = () => {
    const confirmDelete = window.confirm('Are you sure you want to delete this medical record?');
    if (confirmDelete) {
     
      fetch(`/api/MedicalRecord/deleteMedicalRecord/${id}`, {
        method: 'DELETE',
      })
        .then(response => {
          if (!response.ok) {
            throw new Error('Failed to delete medical record');
          }
          setShowMessage(true);
          setDeleteComplete(true);
          setMedicalRecord(null);
        })
        .catch(error => {
          console.error('Error deleting medical record:', error);
        });
    }
  };

  const handleUpdate = () => {
   
    window.location.href = `/medicalRecord/${id}/update`;
  };

  return (
    <div>
      <h1>Medical Record Details</h1>
      {medicalRecord ? (
        <div>
          <MedicalRecordCard medicalRecord={medicalRecord} />
          <Button variant="danger" onClick={handleDelete}>Delete Medical Record</Button>
          {showMessage && <p>Medical record deleted</p>}
          <Button variant="primary" onClick={handleUpdate}>Update Medical Record</Button>
        </div>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
};

export default DisplayMedicalRecord;
