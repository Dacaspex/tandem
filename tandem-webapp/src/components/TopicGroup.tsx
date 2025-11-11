import Topic from './Topic';
import "./Topic.css";
import type {Topic as TopicType} from "../api/users.ts";
import {useApi} from "../api/client.ts";

interface TopicGroupProps {
  title: string;
  topics: TopicType[];
}

/**
 * TopicGroup
 * - Displays a titled box with a list of Topic components
 */
export default function TopicGroup({ title, topics }: TopicGroupProps) {
  const api = useApi();

  const onTopicRatingUpdated = async (topic: TopicType, rating: number) => {
    // TODO: Try catch if fails?
    await api.users.updateTopicRating(topic.id, rating);
  };

  return (
    <div className="topic-group-container">
      <div className="topic-group-title-outer">{title}</div>
      <div className="topic-group-box">
        {topics.map((topic, idx) => (
          <Topic
            key={idx}
            name={topic.name}
            rating={topic.rating}
            onRatingUpdated={ newRating => onTopicRatingUpdated(topic, newRating) }/>
        ))}
      </div>
    </div>
  );
}