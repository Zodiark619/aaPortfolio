import api from "./axios";

export const getProvinces = async (queryParams = {}) => {
  const response = await api.get("/Province", {
    params: queryParams,
  });

  return response.data;
};

export const getProvinceById = async (id) => {
  const response = await api.get(`/Province/${id}`);
  return response.data;
};

export const createProvince = async (province) => {
  const response = await api.post("/Province", province);
  return response.data;
};

export const updateProvince = async (id, province) => {
  const response = await api.put(`/Province/${id}`, province);
  return response.data;
};

export const deleteProvince = async (id) => {
  await api.delete(`/Province/${id}`);
};
export const getProvinceDropdown = async () => {
  const response = await api.get(`/Province/dropdown`);
  return response.data;
};
