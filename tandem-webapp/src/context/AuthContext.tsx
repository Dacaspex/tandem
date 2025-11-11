import {createContext} from "react";

export interface UserType {
  id: string;
  name: string;
}

interface AuthContextType {
  accessToken: string | null;
  login: (username: string, password: string) => Promise<void>;
  logout: () => void;
  authenticatedFetch: (input: RequestInfo | URL, init?: RequestInit) => Promise<Response>;
  authenticatedUser: UserType | null;
}

export const AuthContext = createContext<AuthContextType | undefined>(undefined);

