import DoctorSpecialtyPage from "./pages/DoctorSpecialtyPage";
import DoctorPage from "./pages/DoctorPage";
import { BrowserRouter, Routes, Route } from "react-router";
import Navbar from "./pages/Navbar";
import PatientPage from "./pages/PatientPage";
import ProvincePage from "./pages/ProvincePage";
import AdmissionPage from "./pages/AdmissionPage";
import Login from "./pages/Login";
import Register from "./pages/Register";
import NotFound from "./pages/NotFound";
import HomePage from "./pages/HomePage";
const App = () => {
  return (
    <>
      {/* <div className="container mt-5">
        <DoctorSpecialtyPage />
      </div> */}

      <BrowserRouter>
        <Navbar />
        <div className=" container mt-5">
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/doctorSpecialty" element={<DoctorSpecialtyPage />} />
            <Route path="/doctor" element={<DoctorPage />} />
            <Route path="/province" element={<ProvincePage />} />
            <Route path="/patient" element={<PatientPage />} />
            <Route path="/admission" element={<AdmissionPage />} />
            <Route path="/register" element={<Register />} />
            <Route path="/login" element={<Login />} />

            {/* <Route path="*" element={<NotFound />} /> */}
          </Routes>
        </div>
      </BrowserRouter>
    </>
  );
};

export default App;
