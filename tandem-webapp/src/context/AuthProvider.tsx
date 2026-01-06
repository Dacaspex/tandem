import React, {useEffect, useState} from "react";
import {useNavigate} from "react-router";
import {AuthContext, type UserType} from "./AuthContext";

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({children}) => {
  const [accessToken, setAccessToken] = useState<string | null>(null);
  const [user, setUser] = useState<UserType | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const init = async () => {
      const savedAccessToken = localStorage.getItem("accessToken");
      if (savedAccessToken) {
        setAccessToken(savedAccessToken);
        try {
          await getMe(savedAccessToken);
        } catch {/* ignored */}
      }
    }

    init();
  }, []);

  const login = async (username: string, password: string) => {
    const res = await fetch(`${import.meta.env.VITE_API_BASE_URL}/api/auth/login`, {
      method: "POST",
      headers: {"Content-Type": "application/json"},
      body: JSON.stringify({username, password}),
      credentials: "include",
    });

    if (!res.ok) throw new Error("Login failed");

    const data = await res.json();
    setAccessToken(data.accessToken);
    localStorage.setItem("accessToken", data.accessToken);
    await getMe(data.accessToken);
  };

  const logout = () => {
    setAccessToken(null);
    localStorage.removeItem("accessToken");
    navigate("/login");
  };

  const refreshAccessToken = async (): Promise<string | null> => {
    try {
      const res = await fetch("/api/auth/refresh-token", {
        method: "POST",
        credentials: "include", // send refresh token cookie
      });
      if (!res.ok) throw new Error("Refresh failed");
      const data = await res.json();
      setAccessToken(data.accessToken);
      localStorage.setItem("accessToken", data.accessToken);

      await getMe(data.accessToken);

      return data.accessToken;
    } catch {
      logout();
      return null;
    }
  };

  const getMe = async (accessToken: string) => {
    const userRes = await fetch(`${import.meta.env.VITE_API_BASE_URL}/api/users/me`, {
      headers: { Authorization: `Bearer ${accessToken}` },
    });
    if (userRes.ok) {
      setUser(await userRes.json());
    }
  }

  const authenticatedFetch = async (input: RequestInfo | URL, init: RequestInit = {}) => {
    let tokenToUse = localStorage.getItem("accessToken");

    if (tokenToUse && isJwtExpired(tokenToUse)) {
      tokenToUse = await refreshAccessToken();
      if (!tokenToUse) throw new Error("Session expired");
    }

    const res = await fetch(import.meta.env.VITE_API_BASE_URL + input, {
      ...init,
      headers: {
        ...(init.headers || {}),
        Authorization: tokenToUse ? `Bearer ${tokenToUse}` : "",
        "Content-Type": "application/json"
      },
      credentials: "include",
    });

    if (res.status === 401) {
      // If backend rejects both tokens, log out
      logout();
    }

    return res;
  };

  return (
    <AuthContext.Provider value={{accessToken, login, logout, authenticatedFetch, authenticatedUser: user}}>
      {children}
    </AuthContext.Provider>
  );
}

function isJwtExpired(token: string): boolean {
  try {
    const [, payloadBase64] = token.split(".");
    const payload = JSON.parse(atob(payloadBase64));
    if (!payload.exp) return true;
    return Date.now() >= payload.exp * 1000;
  } catch {
    return true;
  }
}