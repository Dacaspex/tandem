import TopicGroup from "@/components/Topics/TopicGroup.tsx";
import UserCard from "@/components/UserCard.tsx";
import {useEffect, useState} from "react";
import {useApi} from "@/api/client.ts";
import type {UserType} from "@/context/AuthContext.tsx";
import {useAuth} from "@/context/useAuth.ts";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useModal } from "@/context/Modal/ModalContext.tsx";
import CreateTopicGroupModal from "@/Modals/CreateTopicGroupModal.tsx";

function Home() {
  const [editMode, setEditMode] = useState(false);
  const [users, setUsers] = useState<UserType[]>([]);
  const [selectedUserId, setSelectedUserId] = useState<string | null>(null);
  const queryClient = useQueryClient();
  const { open, close } = useModal();
  const { authenticatedUser } = useAuth();
  const api = useApi()

  const {
    data: topicGroups,
    isLoading,
    error
  } = useQuery({
    queryKey: ["topicGroups", selectedUserId],
    queryFn: () => api.users.getTopics(selectedUserId!),
    enabled: selectedUserId !== null,
  });

  const createTopicGroupMutation = useMutation({
    mutationFn: (name: string) => {
      return api.users.createTopicGroup(name);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["topicGroups", selectedUserId],
      });
    },
  });

  useEffect(() => {
    const getUsers = async () => {
      const users = await api.users.getUsers();
      const self = users.find(u => u.id === authenticatedUser?.id);
      if (self && selectedUserId === null) {
        setSelectedUserId(self.id);
      }

      setUsers(users);
    }

    getUsers();
  }, [authenticatedUser]);

  const onUserCardClick = (user: UserType) => {
    setSelectedUserId(user.id);
  }

  const onEditClick = () => {
    setEditMode(true);
  }

  const onConfirmClick = () => {
    setEditMode(false);
  }

  const editActionsJsx = editMode
    ? <button className="button button-inline button-success" onClick={ onConfirmClick }>Confirm</button>
    : <button className="button button-inline button-primary" onClick={ onEditClick }>Edit</button>

  const topicGroupsJsx = topicGroups?.map(group => (
    <TopicGroup
      key={ group.id }
      id={ group.id }
      title={ group.name }
      userId={ selectedUserId! }
      topics={ group.topics }
      editMode={ editMode }
      canChangeRating={ selectedUserId === authenticatedUser?.id}
    />
  ));

  const createTopicGroup = async (name: string) => {
    close();
    await createTopicGroupMutation.mutateAsync(name);
  };

  const onAddTopicGroup = () => {
    open(
      <CreateTopicGroupModal
        onSubmit={createTopicGroup}
        onCancel={close}
      />
    );
  };

  const addTopicGroupJsx = (
    <div
      className="button button-primary button-inline button-outlined"
      onClick={onAddTopicGroup}
    >
      Add topic group
    </div>
  );

  return (
    <>
      <h1>Tandem</h1>

      <div className='flex-row flex-gap-1 mb-1'>
        {
          users.map((user: UserType) => {
            return (
              <div
                key={ user.id }
                onClick={ () => onUserCardClick(user) }>
                <UserCard
                  userName={ user.name }
                  isSelf={ authenticatedUser?.id === user.id }
                  active={ user.id === selectedUserId }
                  lastUpdatedAt={ 'yesterday' }/>
              </div>
            )
          })
        }
      </div>

      {/* topics */}
      <div className="mb-1">
        { isLoading && <div>Loading topics…</div> }
        { error && <div>Failed to load topics</div> }
        { topicGroupsJsx }
      </div>

      { editMode && <div className="mb-1">{ addTopicGroupJsx }</div> }

      <div>{ editActionsJsx }</div>
    </>
  )
}

export default Home;