import api from "./axios";

export const getAdmissions = async (queryParams = {}) => {
  const response = await api.get("/Admission", {
    params: queryParams,
  });

  return response.data;
};

export const getAdmissionById = async (id) => {
  const response = await api.get(`/Admission/${id}`);
  return response.data;
};

export const createAdmission = async (Admission) => {
  const response = await api.post("/Admission", Admission);
  return response.data;
};

export const updateAdmission = async (id, Admission) => {
  const response = await api.put(`/Admission/${id}`, Admission);
  return response.data;
};

export const deleteAdmission = async (id) => {
  await api.delete(`/Admission/${id}`);
};
