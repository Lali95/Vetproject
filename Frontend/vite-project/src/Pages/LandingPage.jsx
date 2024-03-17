import React from 'react';
import { Navbar, Nav } from 'react-bootstrap';
import { Outlet, Link } from 'react-router-dom';
import '../Css/landingPage.css';

const LandingPage = () => {
  const handleGoBack = () => {
    window.history.back();
  };

  return (
    <div className="nav-container"> {/* Add a container class */}
      <Navbar bg="light" expand="lg">
        <Navbar.Brand href="/">Logo</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">
            <Link to="/" className="custom-btn">Home</Link>
            <Link to="/medicalRecord" className="custom-btn">Medical Records</Link>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
      <Outlet />
      <button className="back-button btn btn-primary" onClick={handleGoBack}>
        Back
      </button>
    </div>
  );
};

export default LandingPage;
