import { Link } from "react-router-dom";
import "../index.css";

const LandingPage = () => {



  return (
    <div className="landing-page">
      <header className="logo-header">

      </header>
      <nav className="nav-container">
        <ul className="nav-list">
          <li>
            <Link to="/">Home</Link>
          </li>



        </ul>
      </nav>

    </div>
  );
};

export default LandingPage;
