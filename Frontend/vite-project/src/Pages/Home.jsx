import React from 'react';
import { Button } from 'react-bootstrap';

const Home = () => {
  return (
    <div className="container">
      <h2>Home Page</h2>
      <p>Welcome to the home page!</p>
      <div className="mt-3">
        <Button variant="primary">Log In</Button>{' '}
        <Button variant="success">Sign Up</Button>
      </div>
    </div>
  );
};

export default Home;
