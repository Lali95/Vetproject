import React from 'react';

const MedicalRecord = ({ medicalRecord }) => {
  return (
    <div className="medical-record">
      <h2>Medical Record</h2>
      <p><strong>Vet Name:</strong> {medicalRecord.vetName}</p>
      <p><strong>Owner Name:</strong> {medicalRecord.ownerName}</p>
      <p><strong>Horse Name:</strong> {medicalRecord.horseName}</p>
      <p><strong>Place:</strong> {medicalRecord.place}</p>
      <p><strong>Medical Intervention:</strong> {medicalRecord.medicalIntervention}</p>
      <p><strong>Created At:</strong> {new Date(medicalRecord.createdAt).toLocaleString()}</p>
      <p><strong>Medications:</strong></p>
      <ul>
        {medicalRecord.medicines.map((medicine, index) => (
          <li key={index}>{medicine.name} - {medicine.amount}</li>
        ))}
      </ul>
    </div>
  );
};

export default MedicalRecord;
