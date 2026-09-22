import { BrowserRouter, Routes, Route } from "react-router";
import Home from "./pages/Home.jsx";
import CapstoneProject1 from "./CapstoneProject1/CapstoneProject1.jsx";
import Navbar from "./components/Navbar.jsx";
import CapstoneProject1Contact from "./CapstoneProject1/CapstoneProject1Contact.jsx";

function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/capstone1" element={<CapstoneProject1 />} />
        <Route path="/capstone1contact" element={<CapstoneProject1Contact />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
