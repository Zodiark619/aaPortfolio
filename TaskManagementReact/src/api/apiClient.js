const API_URL = "https://localhost:5001/api";

export const apiFetch = async (endpoint, options = {}) => {
  const user = JSON.parse(localStorage.getItem("user"));

  const response = await fetch(`${API_URL}${endpoint}`, {
    ...options,
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${user?.token}`,
      ...options.headers,
    },
  });

  if (!response.ok) {
    throw new Error(`Request failed: ${response.status}`);
  }

  return response.json();
};
