import Button from "../components/Button.tsx";
import React, {useState} from "react";
import {useAuth} from "../context/useAuth.ts";
import {useNavigate} from "react-router";

function LoginPage() {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await login(username, password);
      navigate('/users');
    } catch {
      alert("Login failed");
    }
  };

  return (
    <>
      <h1>Tandem</h1>
      <div>
        <form onSubmit={onSubmit}>
          <p>Username</p>
          <input
            name='username'
            value={username}
            onChange={(e) => setUsername(e.target.value)}/>
          <p>Password</p>
          <input
            name='password'
            type='password'
            value={password}
            onChange={(e) => setPassword(e.target.value)}/>
          <br/>
          <br/>
          <Button text={"Log in"}/>
        </form>
      </div>
    </>
  );
}

export default LoginPage;