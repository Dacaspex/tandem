import "./Topic.css";
import type {Topic as TopicType} from "../../api/users.ts";
import {useApi} from "@/api/client.ts";
import Topic from "@/components/Topics/Topic.tsx";

interface TopicGroupProps {
  title: string;
  topics: TopicType[];
  editMode: boolean;
}

/**
 * TopicGroup
 * - Displays a titled box with a list of TopicOld components
 */
export default function TopicGroup({ title, topics, editMode }: TopicGroupProps) {
  const api = useApi();

  const onTopicRatingUpdated = async (topic: TopicType, rating: number) => {
    // TODO: Try catch if fails?
    // await api.users.updateTopicRating(topic.id, rating);
    console.log(rating);
  };

  const addTopicButton = (
    <div className="button button-primary button-outlined button-s">Add topic</div>
  )

  return (
    <div className="topic-group-container mb-1">
      <div className="topic-group-title">{title}</div>
      <div className="topic-group-box">
        {topics.map((topic) => (
          <Topic
            key={ topic.id }
            name={ topic.name }
            initialRating={ topic.rating }
            editMode={ editMode }
            onRatingUpdated={ newRating => onTopicRatingUpdated(topic, newRating) }/>
        ))}
        { editMode && addTopicButton }
      </div>
    </div>
  );
}