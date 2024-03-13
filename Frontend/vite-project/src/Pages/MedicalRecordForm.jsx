import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import '../Css/medicalRecords.css';

const MedicalRecordForm = () => {
  const navigate = useNavigate();
  const [medicalRecord, setMedicalRecord] = useState({
    vetName: '',
    ownerName: '',
    horseName: '',
    place: '',
    medications: [],
    medicalIntervention: '',
    createdAt: new Date().toISOString(),
  });

  const [notification, setNotification] = useState({
    type: '',
    message: '',
  });

  const [showConfirmation, setShowConfirmation] = useState(false);
  const [medicines, setMedicines] = useState([]);
  const [selectedMedicine, setSelectedMedicine] = useState('');

  useEffect(() => {
    fetch('/api/Medicine/getMedicines')
      .then(response => response.json())
      .then(data => {
        setMedicines(data);
      })
      .catch(error => console.error('Error fetching medicines:', error));
  }, []);

  const addMedicine = () => {
    const selectedMedicineObject = medicines.find(medicine => medicine.id === parseInt(selectedMedicine));
    if (selectedMedicineObject) {
      setMedicalRecord(prevState => ({
        ...prevState,
        medications: [...prevState.medications, selectedMedicineObject.name], 
      }));
      setSelectedMedicine('');
    }
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    const filledMedicalRecord = {
      vetName: event.target.vetName.value,
      ownerName: event.target.ownerName.value,
      horseName: event.target.horseName.value,
      place: event.target.place.value,
      medications: medicalRecord.medications, 
      medicalIntervention: event.target.medicalIntervention.value,
      createdAt: medicalRecord.createdAt,
    };

    await saveMedicalRecord(filledMedicalRecord);
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
      } else {
        setNotification({ type: 'error', message: 'Error saving medical record.' });
        throw new Error('Error saving medical record.');
      }
    } catch (error) {
      console.error('Error:', error);
      setNotification({ type: 'error', message: 'An unexpected error occurred.' });
    }
  };

  const handleOkClick = () => {
    setShowConfirmation(false);
    setNotification({ type: '', message: '' });
    navigate('/medicalRecord'); 
  };

  return (
    <div>
      <h1>Medical Record</h1>
      {notification.type === 'error' && <p style={{ color: 'red' }}>{notification.message}</p>}
      <form onSubmit={handleSubmit}>
        <div className="form-container">
          <div className="form-group">
            <label htmlFor="vetName">Vet Name:</label>
            <input type="text" id="vetName" name="vetName" required />
          </div>
          <div className="form-group">
            <label htmlFor="ownerName">Owner Name:</label>
            <input type="text" id="ownerName" name="ownerName" required />
          </div>
          <div className="form-group">
            <label htmlFor="horseName">Horse Name:</label>
            <input type="text" id="horseName" name="horseName" required />
          </div>
          <div className="form-group">
            <label htmlFor="place">Place:</label>
            <input type="text" id="place" name="place" required />
          </div>
          <div className="form-group">
            <label htmlFor="medicalIntervention">Medical Intervention:</label>
            <input type="text" id="medicalIntervention" name="medicalIntervention" required />
          </div>
          <div className="form-group">
            <label htmlFor="medicines">Medicines:</label>
            <select
              id="medicines"
              name="medicines"
              value={selectedMedicine}
              onChange={(e) => setSelectedMedicine(e.target.value)}
            >
              <option value="">Select a medicine</option>
              {medicines.map((medicine, index) => (
                <option key={index} value={medicine.id}>{medicine.name}</option>
              ))}
            </select>
            {selectedMedicine && (
              <button type="button" className="add-medicine-btn" onClick={addMedicine}>Add Medicine</button>
            )}
            <ul>
              {medicalRecord.medications.map((medicine, index) => (
                <li key={index}>{medicine}</li>
              ))}
            </ul>
          </div>
        </div>
        <button type="submit" className="save-record-btn">Save Medical Record</button>
      </form>
      {notification.type === 'success' && (
        <div>
          <p style={{ color: 'green' }}>{notification.message}</p>
          <button onClick={handleOkClick} className="modern-button">OK</button>
        </div>
      )}
    </div>
  );
};

export default MedicalRecordForm;
