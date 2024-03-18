import React from 'react';
import { Navbar, Nav } from 'react-bootstrap';
import { Outlet, Link } from 'react-router-dom';
import logo from '../Pictures/logo.png'; // Import your logo image
import '../Css/landingPage.css';

const LandingPage = () => {
  const handleGoBack = () => {
    window.history.back();
  };

  return (
    <div className="nav-container">
      <Navbar bg="light" expand="lg">
        <Navbar.Brand href="/">
          <img
            src={logo}
            alt="Logo"
            className="logo-img" 
          />
        </Navbar.Brand>
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
