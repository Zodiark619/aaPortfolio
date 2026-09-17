import { useActionState, useEffect, useState } from "react";
import { useAuth } from "../context/AuthContext";
import toast from "react-hot-toast";
import { useNavigate } from "react-router";
function Login() {
  const { loginContext } = useAuth();
  const navigate = useNavigate();
  const loginAction = async (previousState, formData) => {
    const email = formData.get("email");
    const password = formData.get("password");

    try {
      const data = await loginContext(email, password);

      toast.success(data.message);
      navigate("/");
    } catch (error) {
      toast.error(error.message);
    }
  };
  const [state, formAction, isPending] = useActionState(loginAction, null);
  return (
    <>
      <h1 className="text-center mb-3">Login</h1>

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

        <button className="btn btn-primary" type="submit" disabled={isPending}>
          {isPending ? "Logging in..." : "Login"}
        </button>
      </form>
    </>
  );
}

export default Login;
