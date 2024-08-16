import React from 'react';
import { Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';

const Home = () => {
  return (
    <div className="container">
      <h2>Home Page</h2>
      <p>Welcome to the home page!</p>
      <div className="mt-3">
        <Link to="/login">
          <Button variant="primary">Log In</Button>
        </Link>
        <Link to="/registration">
          <Button variant="success">Sign Up</Button>
        </Link>
      </div>
    </div>
  );
};

export default Home;
