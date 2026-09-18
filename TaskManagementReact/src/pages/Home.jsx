// pages/Home.jsx
import { useAuth } from "../context/AuthContext";
import { jwtDecode } from "jwt-decode";
function Home() {
  const { user } = useAuth();
  const decoded = user != null ? jwtDecode(user.token) : null;

  return (
    <>
      <h1>Home</h1>
      {user && (
        <>
          <p>
            Logged in as {user.email} - {user.role}
          </p>
        </>
      )}
      {decoded && (
        <>
          <div>
            {/* <p>User identifier: {decoded.nameidentifier}</p>
            <p>User's email: {decoded.emailaddress}</p>
            <p>User's name: {decoded.name}</p>
            <p>User's role: {decoded.role}</p> */}
            <p>
              Expiration time: {new Date(decoded.exp * 1000).toLocaleString()}
            </p>
            {/* <p>Issued-at time: {decoded.iat}</p> */}
            <p>Token issuer: {decoded.iss}</p>
            <p>Intended audience: {decoded.aud}</p>
          </div>
        </>
      )}
    </>
  );
}

export default Home;
