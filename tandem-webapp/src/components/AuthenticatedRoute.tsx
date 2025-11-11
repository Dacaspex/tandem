import {Navigate} from "react-router";
import type {JSX} from "react";
import {useAuth} from "../context/useAuth.ts";

export const PrivateRoute = ({children}: { children: JSX.Element }) => {
  const auth = useAuth();
  console.log(auth)

  if (!auth.accessToken) {
    console.log(auth.accessToken);
    return <Navigate to="/login" />;
  }

  return children;
};