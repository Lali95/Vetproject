import React, { useEffect, useState } from 'react';

const Home = () => {
  const [testName, setTestName] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch('/api/Test/displayTestName');
        const data = await response.json();

        
        setTestName(data.testName);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  }, []); 

  return (
    <div>
      <h2>Home Page</h2>
      <p>Welcome to the home page!</p>
      {testName && (
        <p>
          Test Name: <strong>{testName}</strong>
        </p>
      )}
    </div>
  );
};

export default Home;
