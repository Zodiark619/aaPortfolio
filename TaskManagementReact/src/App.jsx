import { BrowserRouter, Routes, Route } from "react-router";
import Navbar from "./components/Navbar";

import Home from "./pages/Home";
import Register from "./pages/Register";
import Login from "./pages/Login";

{
  /* <Route path="/about" element={<About />} />
<Route path="/contact" element={<Contact />} /> */
}
function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <div className=" container mt-5">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/register" element={<Register />} />
          <Route path="/login" element={<Login />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
