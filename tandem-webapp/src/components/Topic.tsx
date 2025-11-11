import FiveDotsIndicator from "./DotsIndicator.tsx";
import {useRef, useState} from "react";
import './Topic.css';

interface TopicProps {
  name: string;
  rating?: number;
  onRatingUpdated: (rating: number) => void;
}

const colors = [
  "#ef4444", // red-500
  "#f97316", // orange-500
  "#facc15", // yellow-400
  "#a3e635", // lime-400
  "#4ade80", // green-400
];

function toneDown(hex: string, alpha = 0.6): string {
  // convert hex → rgba with given alpha
  const r = parseInt(hex.slice(1, 3), 16);
  const g = parseInt(hex.slice(3, 5), 16);
  const b = parseInt(hex.slice(5, 7), 16);
  return `rgba(${r}, ${g}, ${b}, ${alpha})`;
}

const sliderColors = colors.map((c) => toneDown(c, 0.2));

/**
 * Topic component
 * - Displays a topic name and a 5-dot indicator
 */
export default function Topic({ name, rating = 1, onRatingUpdated }: TopicProps) {
  const [index, setIndex] = useState(rating - 1);
  const [xPos, setXPos] = useState(0);
  const [dragging, setDragging] = useState(false);
  const rowRef = useRef<HTMLDivElement | null>(null);

  const calculateIndex = (clientX: number) => {
    const row = rowRef.current;
    if (!row) return {newIndex: index, relativeX: 0};

    const rect = row.getBoundingClientRect();
    const relativeX = clientX - rect.left;
    const xPercentage = relativeX / rect.width;
    const stepWidth = rect.width / 5; // 5 steps
    let newIndex = Math.floor(relativeX / stepWidth);

    // clamp between 0–4
    newIndex = Math.max(0, Math.min(4, newIndex));
    return { newIndex, relativeX: xPercentage };
  };

  const onPointerDown = (event: React.PointerEvent) => {
    setDragging(true);
    const { newIndex, relativeX } = calculateIndex(event.clientX);

    setIndex(newIndex);
    setXPos(relativeX)
  };

  const onPointerMove = (event: React.PointerEvent) => {
    if (!dragging || event.buttons !== 1) return;
    const { newIndex, relativeX } = calculateIndex(event.clientX);

    setIndex(newIndex);
    setXPos(relativeX)
  };

  const onPointerUp = () => {
    setDragging(false);
    onRatingUpdated(index + 1);
  };

  return (
    <div ref={ rowRef }
         className="topic"
         onPointerDown={ onPointerDown }
         onPointerMove={ onPointerMove }
         onPointerUp={ onPointerUp }
         onPointerLeave={ onPointerUp }>

      {dragging && (
      <div
        className="topic-background"
        style={{
          width: `${xPos * 100}%`,
          backgroundColor: sliderColors[index],
        }}
      />)}

      <div className="topic-content">
        <div className="topic-name">{ name }</div>
        <div className="topic-indicator">
          <FiveDotsIndicator activeIndex={ index } />
        </div>
      </div>
    </div>
  );
}
