
import React, { useState } from "react";


const Registration = () => {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");


  const handleRegistration = async (e) => {
    e.preventDefault();

    const registrationData = {
      Username: username,
      Email: email,
      Password: password
    };

    try {
      const response = await fetch('api/Auth/Register', {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(registrationData),
      });

      if (!response.ok) {
        const errorData = await response.json();
        setErrorMessage(errorData.message);
        return;
      }

 
  
    } catch (error) {
      console.error("Error during registration:", error);
      setErrorMessage("An error occurred during registration. Please try again later.");
    }
  };

  return (
    <div className="registration-container">
      <h4>Please enter your details to register</h4>
      <form onSubmit={handleRegistration}>
        <div>
          <label>
            Username:
            <input
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
            />
          </label>
        </div>
        <div>
          <label>
            Email:
            <input
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </label>
        </div>
        <div>
          <label>
            Password:
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </label>
        </div>
        {errorMessage && <p className="error-message">{errorMessage}</p>}
        <div>
          <button type="submit">Register</button>
        </div>
      </form>
    </div>
  );
};

export default Registration;
