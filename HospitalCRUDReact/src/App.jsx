import DoctorSpecialtyPage from "./pages/DoctorSpecialtyPage";
import DoctorPage from "./pages/DoctorPage";
import { BrowserRouter, Routes, Route } from "react-router";
import Navbar from "./pages/Navbar";
import PatientPage from "./pages/PatientPage";
import ProvincePage from "./pages/ProvincePage";
import AdmissionPage from "./pages/AdmissionPage";
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
            <Route path="/doctorSpecialty" element={<DoctorSpecialtyPage />} />
            <Route path="/doctor" element={<DoctorPage />} />
            <Route path="/province" element={<ProvincePage />} />
            <Route path="/patient" element={<PatientPage />} />
            <Route path="/admission" element={<AdmissionPage />} />

            {/* <Route path="*" element={<NotFound />} /> */}
          </Routes>
        </div>
      </BrowserRouter>
    </>
  );
};

export default App;
