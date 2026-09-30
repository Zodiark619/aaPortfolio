import api from "./axios";

export const getDoctorSpecialties = async (queryParams = {}) => {
  const response = await api.get("/DoctorSpecialty", {
    params: queryParams,
  });

  return response.data;
};

export const getDoctorSpecialtyById = async (id) => {
  const response = await api.get(`/DoctorSpecialty/${id}`);
  return response.data;
};

export const createDoctorSpecialty = async (doctor) => {
  const response = await api.post("/DoctorSpecialty", doctor);
  return response.data;
};

export const updateDoctorSpecialty = async (id, doctor) => {
  const response = await api.put(`/DoctorSpecialty/${id}`, doctor);
  return response.data;
};

export const deleteDoctorSpecialty = async (id) => {
  await api.delete(`/DoctorSpecialty/${id}`);
};
export const getDoctorSpecialtyDropdown = async () => {
  const response = await api.get(`/DoctorSpecialty/dropdown`);
  return response.data;
};
