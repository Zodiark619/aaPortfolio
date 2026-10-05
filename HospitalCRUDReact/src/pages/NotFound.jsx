import { Link } from "react-router";

const NotFound = () => {
  return (
    <div className="container text-center mt-5">
      <h1>404</h1>
      <p>Page not found.</p>
      <Link to="/">Go Home</Link>
    </div>
  );
};
export default NotFound;
