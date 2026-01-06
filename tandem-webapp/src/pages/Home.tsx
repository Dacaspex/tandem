import TopicGroup from "@/components/Topics/TopicGroup.tsx";
import UserCard from "@/components/UserCard.tsx";
import {useState} from "react";

function Home() {
  let [editMode, setEditMode] = useState(false);

  const topicGroups = [
    {
      id: 1,
      name: "Sports",
      topics: [
        {
          id: "a",
          name: "Running",
          rating: 3
        },
        {
          id: "b",
          name: "Bouldering",
          rating: 4
        },
        {
          id: "c",
          name: "Swimming",
          rating: 2
        }
      ]
    },
    {
      id: 2,
      name: "Entertainment",
      topics: [
        {
          id: "a",
          name: "Youtube",
          rating: 3
        },
        {
          id: "b",
          name: "Series",
          rating: 4
        },
      ]
    },
  ]

  const onEditClick = () => {
    setEditMode(true);
  }

  const onConfirmClick = () => {
    setEditMode(false);
  }

  const editActionsJsx = editMode
    ? <button className="button button-inline button-success" onClick={ onConfirmClick }>Confirm</button>
    : <button className="button button-inline button-primary" onClick={ onEditClick }>Edit</button>

  const topicGroupsJsx = topicGroups.map(group => {
    return <TopicGroup
      key={ group.id}
      title={ group.name }
      topics={ group.topics }
      editMode={ editMode }/>
  });

  const addTopicGroupJsx = (
    <div className="button button-primary button-inline button-outlined">Add topic group</div>
  )

  return (
    <>
      <h1>Tandem</h1>

      <div className='flex-row flex-gap-1 mb-1'>
        <UserCard
          userName={ 'Casper' }
          lastUpdatedAt={ 'yesterday' }
          isSelf={ true }
          active={ true }/>
        <UserCard
          userName={ 'Tom' }
          lastUpdatedAt={ 'An hour ago' }
          isSelf={ false }/>
      </div>

      <div className="mb-1">{ topicGroupsJsx }</div>

      { editMode && <div className="mb-1">{ addTopicGroupJsx }</div> }

      <div>{ editActionsJsx }</div>
    </>
  )
}

export default Home;