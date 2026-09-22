import { useEffect } from "react";
import PageTitle from "../components/PageTitle";
import { useNavigate } from "react-router";

const CapstoneProject1 = () => {
  const navigate = useNavigate();
  return (
    <>
      <PageTitle title="Capstone Project 1 | My Resume" />

      <div className="container text-center">
        <div className="row ">
          <h1>Herry Wijaya</h1>
          <img
            className="img mx-auto"
            src="foto.jpg"
            style={{ height: 100, width: 100 }}
          ></img>
        </div>
        <hr></hr>
        <div className="row">
          <h2>Summary</h2>
          <p>
            Software developer with 1 year of experience. Familiar with .NET,
            C#, React, and SQL.
          </p>
        </div>
        <div className="row">
          <h2>Work Experience</h2>
          <ul className="list-group">
            <li className="list-group-item">
              <h3>Software Developer - Mede Media Softika</h3>
              <p>April 2025 - June 2025</p>
              <ul className="list-group">
                <li className="list-group-item">
                  Using Java and Springboot to create Services in backend using
                  MVC
                </li>
                <li className="list-group-item">
                  Create and modify SQL stored procedure and views from existing
                  database
                </li>
                <li className="list-group-item">
                  Serve API in frontend using AJAX
                </li>
              </ul>
            </li>
            {/*  */}

            <li className="list-group-item">
              <h3>Test Environment Engineer - BNI SKTI</h3>
              <p>December 2023 - June 2024</p>
              <ul className="list-group">
                <li className="list-group-item">
                  Configure multiple change request deployments such as .NET's
                  IIS and Java's .jar in testing server
                </li>
                <li className="list-group-item">
                  Configure multiple API deployments using Axway Policy Manager
                </li>
                <li className="list-group-item">
                  Configure database deployments such as stored procedure in
                  Oracle, PostgreSQL, and SQL Server
                </li>
              </ul>
            </li>
            {/*  */}
            <li className="list-group-item">
              <h3>.NET Developer - Adicipta Inovasi Teknologi</h3>
              <p>December 2022 - June 2023</p>
              <ul className="list-group">
                <li className="list-group-item">
                  Using ASP.NET 4 and Webforms with C# and VB.NET to handle
                  change requests
                </li>
                <li className="list-group-item">
                  Create and modify SQL stored procedure and Crystal Report's
                  .rpt
                </li>
                <li className="list-group-item">
                  Create user manual documents for documentation during
                  deployment phase
                </li>
              </ul>
            </li>
          </ul>
        </div>
        <div className="row">
          <h2>Education</h2>
          <ul className="list-group">
            <li className="list-group-item">
              Bachelor Degree of Computer Science
            </li>
          </ul>
        </div>

        <div className="row">
          <h2>Skills</h2>
          <ul className="list-group">
            <li className="list-group-item">.NET</li>
            <li className="list-group-item">React</li>
            <li className="list-group-item">C#</li>
            <li className="list-group-item">SQL</li>
          </ul>
        </div>
        <button onClick={() => navigate("/capstone1contact")}>Contacts</button>
        <footer className="bg-dark text-white text-center py-4">
          <p>Herry Wijaya. All rights reserved.</p>
        </footer>
      </div>
    </>
  );
};

export default CapstoneProject1;
