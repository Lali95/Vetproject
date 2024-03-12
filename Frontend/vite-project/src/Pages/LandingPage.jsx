import { Navbar, Nav } from 'react-bootstrap';
import { Outlet, Link } from 'react-router-dom';
import '../Css/landingPage.css';

const LandingPage = () => {
  return (
    <div>
      <Navbar bg="light" expand="lg">
        <Navbar.Brand href="/">Your Logo</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="mr-auto">
         
            <Link to="/" className="custom-btn">Home</Link>
            <Link to="/medicalRecord" className="custom-btn">Medical Record</Link>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
      <Outlet />
    </div>
  );
};

export default LandingPage;
