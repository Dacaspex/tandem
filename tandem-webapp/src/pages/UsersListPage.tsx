import UserCard from "../components/UserCard.tsx";
import {NavLink} from "react-router";
import {useApi} from "../api/client.ts";
import {useEffect, useState} from "react";
import {useAuth} from "../context/useAuth.ts";
import type {UserType} from "../context/AuthContext.tsx";

function UsersListPage() {
  const { authenticatedUser } = useAuth();
  const api = useApi();
  const [users, setUsers] = useState<UserType[]>([]);

  useEffect(() => {
    const getUsers = async () => {
      const users = await api.users.getUsers();
      setUsers(users);
    }

    getUsers();
  }, []);

  const userCards = users.map(u => {
    const link = `/users/${u.id}`;
    const isSelf = authenticatedUser?.id === u.id;
    return (
      <NavLink to={ link }>
        <UserCard userName={ u.name } isSelf={ isSelf } lastUpdatedAt={ 'TODO' }/>
      </NavLink>
    )
  });

  return (
    <>
      <h1>Tandem</h1>
      <div style={{ display: 'flex', flexDirection:'column', gap: '0.75rem'}}>{ userCards }</div>
    </>
  )
}

export default UsersListPage;