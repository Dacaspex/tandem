import {LuChevronLeft, LuChevronRight, LuPencil, LuTrash} from "react-icons/lu";
import './Topic.css';
import FiveDotsIndicator from "@/components/Topics/DotsIndicator.tsx";
import {useState} from "react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import {useApi} from "@/api/client.ts";

interface TopicProps {
  id: string;
  name: string;
  initialRating: number;
  userId: string;
  editMode: boolean;
  canChangeRating: boolean;
  onRatingUpdated: (rating: number) => void;
}

export default function Topic({ id, name, initialRating, userId, editMode, canChangeRating, onRatingUpdated }: TopicProps) {
  const [rating, setRating] = useState(initialRating);
  const api = useApi();
  const queryClient = useQueryClient();

  const deleteTopicMutation = useMutation({
    mutationFn: (id: string) => {
      return api.users.deleteTopic(id);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["topicGroups", userId],
      });
    }
  });

  const onRatingIncreased = () => {
    if (rating >= 5) return;
    setRating(rating + 1);
    onRatingUpdated(rating + 1);
  }

  const onRatingDecreased = () => {
    if (rating <= 1) return;
    setRating(rating - 1);
    onRatingUpdated(rating - 1);
  }

  const deleteTopic = () => {
    deleteTopicMutation.mutate(id);
  }

  const increaseClasses = rating < 5
    ? "topic-increase"
    : "topic-increase disabled";
  const decreaseClasses = rating > 1
    ? "topic-decrease"
    : "topic-decrease disabled";

  const topicActions = editMode
    ? (
      <>
        <div className="topic-action"><LuPencil/></div>
        <div
          className="topic-action text-danger"
          onClick={ deleteTopic }>
          <LuTrash/>
        </div>
      </>
    )
    : (
      <>
        { canChangeRating
          ? <div className={ decreaseClasses } onClick={ onRatingDecreased }><LuChevronLeft/></div>
          : <div className={ decreaseClasses } onClick={ onRatingDecreased }/> }
        <div className="topic-rating"><FiveDotsIndicator activeIndex={ rating }/></div>
        { canChangeRating
          ? <div className={ increaseClasses } onClick={ onRatingIncreased }><LuChevronRight/></div>
          : <div className={ increaseClasses } onClick={ onRatingIncreased }/> }
      </>
    )

  return (
    <div className="topic">
      <div className="topic-name">{ name }</div>
      { topicActions }
    </div>
  )
}