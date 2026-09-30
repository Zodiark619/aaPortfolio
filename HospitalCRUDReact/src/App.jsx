import DoctorSpecialtyPage from "./pages/DoctorSpecialtyPage";
import DoctorPage from "./pages/DoctorPage";
import { BrowserRouter, Routes, Route } from "react-router";
import Navbar from "./pages/Navbar";
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

            {/* <Route path="*" element={<NotFound />} /> */}
          </Routes>
        </div>
      </BrowserRouter>
    </>
  );
};

export default App;
