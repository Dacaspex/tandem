import {NavLink, useParams} from "react-router";
import Button from "../components/Button.tsx";
import {useApi} from "../api/client.ts";
import {useEffect, useState} from "react";
import type {TopicGroup as TopicGroupType} from "../api/users.ts";
import TopicGroup from "../components/TopicGroup.tsx";
import {useAuth} from "../context/useAuth.ts";
import type {UserType} from "../context/AuthContext.tsx";

function UserTopicsPage() {
  const params = useParams();
  const { authenticatedUser } = useAuth();
  const api = useApi();

  const [topicGroups, setTopicGroups] = useState<TopicGroupType[]>([]);
  const [pageUser, setPageUser] = useState<UserType>();

  useEffect(() => {
    const load = async () => {
      if (params.userId == null)
      {
        return;
      }
      const pageUser = await api.users.getUser(params.userId);
      setPageUser(pageUser);

      const result = await api.users.getTopics(params.userId);
      setTopicGroups(result);
    };

    load();
  }, []);

  const topicGroupsView = topicGroups.map(tg => {
    return(
      <>
        <TopicGroup title={ tg.name } topics={ tg.topics }/>
        <div style={{ padding: '15px' }}/>
      </>
    )
  });

  return (
    <>
      <h1>Tandem</h1>
      <h3>Casper (you)</h3>
      <NavLink to='/users'>
        <Button text={ 'Back' }/>
      </NavLink>

      <div style={{ padding: '15px' }}/>
      <div className='test'>
        { topicGroupsView }
      </div>
    </>
  )
}

export default UserTopicsPage;