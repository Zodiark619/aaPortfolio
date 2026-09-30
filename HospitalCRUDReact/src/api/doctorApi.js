import api from "./axios";

export const getDoctor = async (queryParams = {}) => {
  const response = await api.get("/Doctor", {
    params: queryParams,
  });

  return response.data;
};

export const getDoctorById = async (id) => {
  const response = await api.get(`/Doctor/${id}`);
  return response.data;
};

export const createDoctor = async (doctor) => {
  const response = await api.post("/Doctor", doctor);
  return response.data;
};

export const updateDoctor = async (id, doctor) => {
  const response = await api.put(`/Doctor/${id}`, doctor);
  return response.data;
};

export const deleteDoctor = async (id) => {
  await api.delete(`/Doctor/${id}`);
};
