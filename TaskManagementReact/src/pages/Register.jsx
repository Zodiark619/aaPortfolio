import { useActionState, useEffect, useState } from "react";
import { getRoles, register } from "../api/auth";
import toast from "react-hot-toast";
function Register() {
  const [roles, setRoles] = useState([]);
  useEffect(() => {
    const loadRoles = async () => {
      try {
        const data = await getRoles();
        setRoles(data);
      } catch (error) {
        console.error(error);
      }
    };

    loadRoles();
  }, []);

  const registerAction = async (previousState, formData) => {
    const email = formData.get("email");
    const password = formData.get("password");
    const role = formData.get("role");
    try {
      const data = await register(email, password, role);
      toast.success(data.message);
      return {
        success: data.success,
        message: data.message,
      };
    } catch (error) {
      toast.error(error.message);
      return {
        success: false,
        message: error.message,
      };
    }
  };
  const [state, formAction, isPending] = useActionState(registerAction, null);
  return (
    <>
      <h1 className="text-center mb-3">Register</h1>

      <form action={formAction}>
        <div className="mb-3">
          <label className="form-label">Email Address</label>
          <input
            className="form-control"
            type="email"
            name="email"
            placeholder="Email"
            required
            autoComplete="email"
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Password</label>

          <input
            className="form-control"
            type="password"
            name="password"
            placeholder="Password"
            required
            autoComplete="new-password"
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Password</label>
          <select className="form-control" name="role" required>
            {roles.map((role) => (
              <option key={role.name} value={role.name}>
                {role.name}
              </option>
            ))}
          </select>
        </div>
        <button className="btn btn-primary" type="submit" disabled={isPending}>
          {isPending ? "Registering in..." : "Register"}
        </button>
      </form>
    </>
  );
}

export default Register;
