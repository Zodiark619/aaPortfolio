import api from "./axios";

export const getPatients = async (queryParams = {}) => {
  const response = await api.get("/Patient", {
    params: queryParams,
  });

  return response.data;
};

export const getPatientById = async (id) => {
  const response = await api.get(`/Patient/${id}`);
  return response.data;
};

export const createPatient = async (Patient) => {
  const response = await api.post("/Patient", Patient);
  return response.data;
};

export const updatePatient = async (id, Patient) => {
  const response = await api.put(`/Patient/${id}`, Patient);
  return response.data;
};

export const deletePatient = async (id) => {
  await api.delete(`/Patient/${id}`);
};
export const getPatientDropdown = async () => {
  const response = await api.get(`/Patient/dropdown`);
  return response.data;
};
