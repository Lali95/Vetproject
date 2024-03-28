import React, { useState, useEffect } from 'react';
import { Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import '../Css/searchMedicalRecord.css'; 

const SearchMedicalRecord = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [filteredRecords, setFilteredRecords] = useState([]);
  const [sortConfig, setSortConfig] = useState({ key: '', direction: '' });
  const navigate = useNavigate();

  useEffect(() => {
    fetch('/api/MedicalRecord/getAllMedicalRecords')
      .then(response => response.json())
      .then(data => {
        setFilteredRecords(data);
      })
      .catch(error => console.error('Error fetching medical records:', error));
  }, []);

  const handleSearch = () => {
    fetch(`/api/MedicalRecord/searchMedicalRecords?searchTerm=${searchTerm}`)
      .then(response => {
        if (!response.ok) {
          throw new Error('Failed to fetch data');
        }
        return response.json();
      })
      .then(data => {
        setFilteredRecords(data);
      })
      .catch(error => {
        console.error('Error fetching medical records:', error);
      });
  };

  const handleSort = (key) => {
    const direction = sortConfig.key === key && sortConfig.direction === 'asc' ? 'desc' : 'asc';
    setSortConfig({ key, direction });

    const sortedRecords = [...filteredRecords].sort((a, b) => {
      if (a[key] < b[key]) return direction === 'asc' ? -1 : 1;
      if (a[key] > b[key]) return direction === 'asc' ? 1 : -1;
      return 0;
    });

    setFilteredRecords(sortedRecords);
  };

  const handleSelectRecord = (id) => {
    navigate(`/medicalRecord/${id}`);
  };

  return (
    <div className="search-medical-record">
      <h1>Medical Records</h1>
      
      <div className="search-container">
        <label htmlFor="searchTerm">Search:</label>
        <input
          type="text"
          id="searchTerm"
          placeholder="Enter search term"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
        <Button variant="success" onClick={handleSearch}>Search</Button>
      </div>

      <div className="sorting-buttons">
        <Button variant="info" style={{ width: '200px' }} onClick={() => handleSort('createdAt')}>
          Order by Date {sortConfig.key === 'createdAt' ? `(${sortConfig.direction === 'asc' ? '↑' : '↓'})` : ''}
        </Button>
        <Button variant="info" style={{ width: '200px' }} onClick={() => handleSort('horseName')}>
          Order by Horse Name {sortConfig.key === 'horseName' ? `(${sortConfig.direction === 'asc' ? '↑' : '↓'})` : ''}
        </Button>
        <Button variant="info" style={{ width: '200px' }} onClick={() => handleSort('ownerName')}>
          Order by Owner Name {sortConfig.key === 'ownerName' ? `(${sortConfig.direction === 'asc' ? '↑' : '↓'})` : ''}
        </Button>
      </div>
  
      <div className="record-container">
        {filteredRecords.map(record => (
          <div className="record" key={record.id} onClick={() => handleSelectRecord(record.id)}>
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
