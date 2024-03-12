import React, { useState, useEffect } from 'react';
import '../Css/searchMedicalRecord.css'; // Import CSS file for styling

const SearchMedicalRecord = () => {
  const [medicalRecords, setMedicalRecords] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [filteredRecords, setFilteredRecords] = useState([]);

  useEffect(() => {
    // Fetch all medical records on component mount
    fetch('/api/MedicalRecord/getAllMedicalRecords')
      .then(response => response.json())
      .then(data => {
        setMedicalRecords(data);
        setFilteredRecords(data);
      })
      .catch(error => console.error('Error fetching medical records:', error));
  }, []);

  const handleSearch = () => {
    const searchTermLower = searchTerm.toLowerCase();
    const filtered = medicalRecords.filter(record =>
      record.ownerName.toLowerCase().includes(searchTermLower) ||
      record.vetName.toLowerCase().includes(searchTermLower) ||
      record.horseName.toLowerCase().includes(searchTermLower) ||
      new Date(record.createdAt).toLocaleDateString().includes(searchTermLower)
    );
    setFilteredRecords(filtered);
  };

  return (
    <div>
      <h1>Medical Records</h1>
      
      <div className="search-container">
        <label htmlFor="ownerSearch">Search for Owner, Vet, Horse, or Date: </label>
        <input
          type="text"
          id="ownerSearch"
          placeholder="Enter search term"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
        <button className="search-button" onClick={handleSearch}>Search</button>
      </div>
  
      <div className="record-container">
        {filteredRecords.map(record => (
          <div className="record" key={record.id}>
            <div><strong>Vet Name:</strong> {record.vetName}</div>
            <div><strong>Owner Name:</strong> {record.ownerName}</div>
            <div><strong>Horse Name:</strong> {record.horseName}</div>
            <div><strong>Creation date:</strong> {new Date(record.createdAt).toLocaleString()}</div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default SearchMedicalRecord;
