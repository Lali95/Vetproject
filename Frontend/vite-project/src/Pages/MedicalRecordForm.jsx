import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from 'bootstrap';

const MedicalRecordForm = () => {
  const navigate = useNavigate();
  const [medicalRecord, setMedicalRecord] = useState({
    vetName: '',
    ownerName: '',
    horseName: '',
    place: '',
    medication: '',
    medicalIntervention: '',
  });

  const [notification, setNotification] = useState({
    type: '',
    message: '',
  });

  const [showConfirmation, setShowConfirmation] = useState(false);

  const fetchEmptyMedicalRecord = () => {
    fetch('/api/MedicalRecord/getEmptyMedicalRecord')
      .then(response => response.json())
      .then(data => {
        setMedicalRecord(data);
        setNotification({ type: '', message: '' });
        setShowConfirmation(false);
      })
      .catch(error => {
        console.error('Error:', error);
        setNotification({ type: 'error', message: 'Error fetching an empty medical record. Please try again.' });
        setShowConfirmation(false);
      });
  };

  const saveMedicalRecord = async (filledMedicalRecord) => {
    try {
      const response = await fetch('/api/MedicalRecord/SaveMedicalRecord', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(filledMedicalRecord),
      });

      if (response.status === 200) {
        setNotification({ type: 'success', message: 'Medical Record saved successfully.' });
        setShowConfirmation(true);
        // Handle data if needed
      } else {
        setNotification({ type: 'error', message: 'Error saving medical record.' });
        throw new Error('Error saving medical record.');
      }
    } catch (error) {
      console.error('Error:', error);
      setNotification({ type: 'error', message: 'An unexpected error occurred.' });
    }
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    const filledMedicalRecord = {
      vetName: event.target.vetName.value,
      ownerName: event.target.ownerName.value,
      horseName: event.target.horseName.value,
      place: event.target.place.value,
      medication: event.target.medication.value,
      medicalIntervention: event.target.medicalIntervention.value,
    };

    await saveMedicalRecord(filledMedicalRecord);
  };

  useEffect(() => {
    if (notification.type === 'success') {
      setShowConfirmation(true);
    }
  }, [notification.type]);

  const handleOkClick = () => {
    setShowConfirmation(false);
    setNotification({ type: '', message: '' });
    navigate('/medicalRecord'); 
  };

  return (
    <div>
      <h1>{showConfirmation ? 'Success' : 'Medical Record'}</h1>
      {notification.type === 'error' && <p style={{ color: 'red' }}>{notification.message}</p>}
      {showConfirmation && (
        <div>
          <p style={{ color: 'green' }}>{notification.message}</p>
          <button onClick={handleOkClick}>OK</button>
        </div>
      )}
      {!showConfirmation && (
        <form onSubmit={handleSubmit}>
          {Object.keys(medicalRecord).map((field, index) => (
            <div key={index}>
              <label htmlFor={field}>{field.charAt(0).toUpperCase() + field.slice(1)}:</label>
              <input type="text" id={field} name={field} defaultValue={medicalRecord[field]} required />
            </div>
          ))}
          <button type="submit">Save Medical Record</button>
        </form>
      )}
    </div>
  );
};

export default MedicalRecordForm;
