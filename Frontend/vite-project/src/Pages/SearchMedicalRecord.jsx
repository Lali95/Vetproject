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
      record.ownerName.toLowerCase().includes(searchTermLower)
    );
    setFilteredRecords(filtered);
  };

  return (
    <div>
      <h1>Medical Records</h1>
      
      <div className="search-container">
        <label htmlFor="ownerSearch">Search for Owner: </label>
        <input
          type="text"
          id="ownerSearch"
          placeholder="Enter owner's name"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
        <button className="search-button" onClick={handleSearch}>Search</button>
      </div>

      <table>
        <thead>
          <tr>
            <th>Vet Name</th>
            <th>Owner Name</th>
            <th>Horse Name</th>
          </tr>
        </thead>
        <tbody>
          {filteredRecords.map(record => (
            <tr key={record.id}>
              <td>{record.vetName}</td>
              <td>{record.ownerName}</td>
              <td>{record.horseName}</td>
              {/* Add more columns as needed */}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default SearchMedicalRecord;
