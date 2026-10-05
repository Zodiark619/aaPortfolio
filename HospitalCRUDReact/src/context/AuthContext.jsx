import { createContext, useContext, useState } from "react";
import { login } from "../api/authApi";
const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [user, setUser] = useState(() => {
    const storedUser = localStorage.getItem("user");
    return storedUser ? JSON.parse(storedUser) : null;
  });

  const loginContext = async (email, password) => {
    const data = await login(email, password);
    setUser(data);
    localStorage.setItem("user", JSON.stringify(data));

    return data;
  };

  const logout = () => {
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ user, loginContext, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  return useContext(AuthContext);
}
