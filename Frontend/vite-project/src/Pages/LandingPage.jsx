
import "../index.css";
import { Outlet, Link } from "react-router-dom";

const LandingPage = () => {



  return (
    <div className="landing-page">
      <header className="logo-header">

      </header>
      <nav className="nav-container">
        <ul className="nav-list">
          <li>
            <Link to="/home">Home</Link>
          </li>



        </ul>
      </nav>
      <Outlet />
    </div>
  );
};

export default LandingPage;
