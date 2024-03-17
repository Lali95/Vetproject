import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

const UpdateMedicalRecord = () => {
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
        // Optionally, handle success scenario (e.g., display success message)
      })
      .catch(error => {
        console.error('Error updating medical record:', error);
        // Optionally, handle error scenario (e.g., display error message)
      });
  };


  return (
    <div>
      <h1>Update Medical Record</h1>
      {medicalRecord ? (
        <form>
          <div>
            <label>Vet Name:</label>
            <input
              type="text"
              value={medicalRecord.vetName}
              onChange={e => handleFieldChange('vetName', e.target.value)}
            />
          </div>
          <div>
            <label>Owner Name:</label>
            <input
              type="text"
              value={medicalRecord.ownerName}
              onChange={e => handleFieldChange('ownerName', e.target.value)}
            />
          </div>
          <div>
            <label>Horse Name:</label>
            <input
              type="text"
              value={medicalRecord.horseName}
              onChange={e => handleFieldChange('horseName', e.target.value)}
            />
          </div>
          <div>
            <label>Place:</label>
            <input
              type="text"
              value={medicalRecord.place}
              onChange={e => handleFieldChange('place', e.target.value)}
            />
          </div>
          <div>
            <label>Medical Intervention:</label>
            <input
              type="text"
              value={medicalRecord.medicalIntervention}
              onChange={e => handleFieldChange('medicalIntervention', e.target.value)}
            />
          </div>
          <div>
            <label>Created At:</label>
            <input
              type="text"
              value={medicalRecord.createdAt}
              readOnly
            />
          </div>
          <div>
            <label>List of Medicines:</label>
            <textarea
              value={medicalRecord.medicines}
              onChange={e => handleFieldChange('medicines', e.target.value)}
            />
          </div>
          {/* Repeat similar structure for other fields */}
          <button type="button" onClick={handleSave}>Save Changes</button>
        </form>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
  
  
  
};

export default UpdateMedicalRecord;
