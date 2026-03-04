import React, {useState} from "react";
import {useAuth} from "../context/useAuth.ts";
import {useNavigate} from "react-router";

function LoginPage() {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const onSubmit = async () => {
    try {
      await login(username, password);
      navigate('/');
    } catch {
      alert("Login failed");
    }
  };

  return (
    <>
      <h1>Tandem</h1>
      <div>
        <form onSubmit={ onSubmit }>
          <p>Username</p>
          <input
            className="form-control form-control-inline"
            name='username'
            value={ username }
            onChange={ (e) => setUsername(e.target.value) }/>
          <p>Password</p>
          <input
            className="form-control form-control-inline"
            name='password'
            type='password'
            value={ password }
            onChange={ (e) => setPassword(e.target.value) }/>
          <br/>
          <br/>
          <div
            className="button button-primary button-inline"
            onClick={ onSubmit }>
            Log in
          </div>
        </form>
      </div>
    </>
  );
}

export default LoginPage;