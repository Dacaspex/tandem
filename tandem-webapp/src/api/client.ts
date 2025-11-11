import {useAuth} from "../context/useAuth.ts";
import * as UserApi from './users.ts';

export function useApi() {
  const { authenticatedFetch } = useAuth();

  return {
    users: UserApi.create(authenticatedFetch)
  }
}