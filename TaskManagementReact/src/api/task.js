// export const getAll = async (token, page = 1, pageSize = 3) => {
//   const response = await fetch(
//     `https://localhost:5001/api/todotask?page=${page}&pageSize=${pageSize}`,
//     {
//       method: "GET",
//       headers: {
//         "Content-Type": "application/json",
//         Authorization: `Bearer ${token}`,
//       },
//     },
//   );

//   if (!response.ok) {
//     throw new Error(`Fetch all failed: ${response.status}`);
//   }

//   const data = await response.json();

//   return data;
// };
// export const getById = async (token, id) => {
//   const response = await fetch(`https://localhost:5001/api/todotask/${id}`, {
//     method: "GET",
//     headers: {
//       "Content-Type": "application/json",
//       Authorization: `Bearer ${token}`,
//     },
//   });

//   if (!response.ok) {
//     throw new Error("Fetch by id failed");
//   }

//   const data = await response.json();

//   return data;
// };
import { apiFetch } from "./apiClient";

export const getAll = async (page = 1, pageSize = 3) => {
  return apiFetch(`/todotask?page=${page}&pageSize=${pageSize}`);
};

export const getById = async (id) => {
  return apiFetch(`/todotask/${id}`);
};

export const createTask = async (task) => {
  return apiFetch("/todotask", {
    method: "POST",
    body: JSON.stringify(task),
  });
};
