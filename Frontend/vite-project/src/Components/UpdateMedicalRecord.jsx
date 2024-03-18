import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

const UpdateMedicalRecord = () => {
  const { id } = useParams();
  const [medicalRecord, setMedicalRecord] = useState(null);
  const [showSuccessMessage, setShowSuccessMessage] = useState(false);

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

  const handleFieldChange = (fieldName, newValue) => {
    setMedicalRecord(prevRecord => ({
      ...prevRecord,
      [fieldName]: newValue
    }));
  };

  const handleSave = () => {
    // Make PUT request to update the entire medical record
    fetch(`/api/MedicalRecord/updateMedicalRecord/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(medicalRecord)
    })
      .then(response => {
        if (!response.ok) {
          throw new Error('Failed to update medical record');
        }
        setShowSuccessMessage(true); // Display success message
      })
      .catch(error => {
        console.error('Error updating medical record:', error);
      });
  };


  return (
    <div>
      <h1>Update Medical Record</h1>
      {showSuccessMessage && <p className="text-success">Medical record updated successfully!</p>}
      {medicalRecord ? (
        <form>
          <div className="mb-3">
            <label className="form-label">Vet Name:</label>
            <input
              type="text"
              className="form-control"
              value={medicalRecord.vetName}
              onChange={e => handleFieldChange('vetName', e.target.value)}
            />
          </div>
          <div className="mb-3">
            <label className="form-label">Owner Name:</label>
            <input
              type="text"
              className="form-control"
              value={medicalRecord.ownerName}
              onChange={e => handleFieldChange('ownerName', e.target.value)}
            />
          </div>
          <div className="mb-3">
            <label className="form-label">Horse Name:</label>
            <input
              type="text"
              className="form-control"
              value={medicalRecord.horseName}
              onChange={e => handleFieldChange('horseName', e.target.value)}
            />
          </div>
          <div className="mb-3">
            <label className="form-label">Place:</label>
            <input
              type="text"
              className="form-control"
              value={medicalRecord.place}
              onChange={e => handleFieldChange('place', e.target.value)}
            />
          </div>
          <div className="mb-3">
            <label className="form-label">Medical Intervention:</label>
            <input
              type="text"
              className="form-control"
              value={medicalRecord.medicalIntervention}
              onChange={e => handleFieldChange('medicalIntervention', e.target.value)}
            />
          </div>
          <div className="mb-3">
            <label className="form-label">Created At:</label>
            <input
              type="text"
              className="form-control"
              value={medicalRecord.createdAt}
              readOnly
            />
          </div>
          <div className="mb-3">
            <label className="form-label">List of Medicines:</label>
            <textarea
              className="form-control"
              value={medicalRecord.medicines}
              onChange={e => handleFieldChange('medicines', e.target.value)}
            />
          </div>
          <button type="button" className="btn btn-primary" onClick={handleSave}>Save Changes</button>
        </form>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
  
  
  
  
  
};

export default UpdateMedicalRecord;
