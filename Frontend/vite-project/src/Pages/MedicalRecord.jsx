import React, { useState } from 'react';

const MedicalRecord = () => {
  // State to hold form data
  const [formData, setFormData] = useState({
    ownerName: '',
    horseName: '',
  });

  // Handle form field changes
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  // Handle form submission
  const handleSubmit = (e) => {
    e.preventDefault();
    // Add logic for handling form submission (e.g., send data to server)
    console.log('Form submitted:', formData);
  };

  return (
    <div>
      <h2>Medical Record Form</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="ownerName">Owner's Name:</label>
          <input
            type="text"
            id="ownerName"
            name="ownerName"
            value={formData.ownerName}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label htmlFor="horseName">Horse's Name:</label>
          <input
            type="text"
            id="horseName"
            name="horseName"
            value={formData.horseName}
            onChange={handleChange}
            required
          />
        </div>
        <button type="submit">Submit</button>
      </form>
    </div>
  );
};

export default MedicalRecord;
