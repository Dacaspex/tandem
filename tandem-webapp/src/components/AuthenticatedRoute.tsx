import {Navigate} from "react-router";
import type {JSX} from "react";
import {useAuth} from "../context/useAuth.ts";

export const PrivateRoute = ({children}: { children: JSX.Element }) => {
  const auth = useAuth();

  if (!auth.accessToken) {
    return <Navigate to="/login" />;
  }

  return children;
};