export const login = async (email, password) => {
  const response = await fetch("https://localhost:5001/api/auth/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      email,
      password,
    }),
  });

  const data = await response.json();

  if (!response.ok) {
    throw new Error(data.message || "Login failed");
  }

  return data;
};

export const register = async (email, password, role) => {
  const response = await fetch("https://localhost:5001/api/auth/register", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      email,
      password,
      role,
    }),
  });

  const data = await response.json();
  if (!response.ok) {
    throw new Error(data.message || "Register failed");
  }

  if (!data.success) {
    throw new Error(data.message);
  }

  return data;
};

export const getRoles = async () => {
  const response = await fetch("https://localhost:5001/api/auth/getRoles");

  if (!response.ok) {
    throw new Error("Error fetching roles");
  }

  const data = await response.json();
  return data;
};
