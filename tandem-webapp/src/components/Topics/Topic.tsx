import {LuChevronLeft, LuChevronRight, LuPencil, LuTrash} from "react-icons/lu";
import './Topic.css';
import FiveDotsIndicator from "@/components/Topics/DotsIndicator.tsx";
import {useState} from "react";

interface TopicProps {
  name: string;
  initialRating: number;
  editMode: boolean;
  onRatingUpdated: (rating: number) => void;
}

export default function Topic({ name, initialRating, editMode, onRatingUpdated }: TopicProps) {
  const [rating, setRating] = useState(initialRating);

  const onRatingIncreased = () => {
    if (rating >= 4) return;
    setRating(rating + 1);
    onRatingUpdated(rating + 1);
  }

  const onRatingDecreased = () => {
    if (rating <= 0) return;
    setRating(rating - 1);
    onRatingUpdated(rating - 1);
  }

  const increaseClasses = rating < 4
    ? "topic-increase"
    : "topic-increase disabled";
  const decreaseClasses = rating > 0
    ? "topic-decrease"
    : "topic-decrease disabled";

  const topicActions = editMode
    ? (
      <>
        <div className="topic-action"><LuPencil/></div>
        <div className="topic-action text-danger"><LuTrash/></div>
      </>
    )
    : (
      <>
        <div className={decreaseClasses} onClick={onRatingDecreased}><LuChevronLeft/></div>
        <div className="topic-rating"><FiveDotsIndicator activeIndex={rating}/></div>
        <div className={increaseClasses} onClick={onRatingIncreased}><LuChevronRight/></div>
      </>
    )

  return (
    <div className="topic">
      <div className="topic-name">{ name }</div>
      { topicActions }
    </div>
  )
}