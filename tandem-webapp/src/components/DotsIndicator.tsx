import React from "react";

interface DotsIndicatorProps {
  /** Which dot is larger (0-based index, 0..4). Defaults to 0 */
  activeIndex?: number;
  /** Base diameter of each dot in pixels (for inactive dots). Defaults to 10 */
  size?: number;
  /** Scale applied to the active dot. Defaults to 1.6 */
  activeScale?: number;
  /** Horizontal gap between dots in pixels. Defaults to 10 */
  gap?: number;
  /** Dot color, defaults to #444 */
  color?: string;
  /** Optional wrapper style */
  style?: React.CSSProperties;
}


/**
 * FiveDotsIndicator
 *
 * - Renders 5 dots horizontally, vertically centered.
 * - One dot (controlled by activeIndex) is slightly bigger.
 * - No animation dependencies (no framer-motion).
 *
 * Example:
 * <FiveDotsIndicator activeIndex={2} size={12} activeScale={1.7} />
 */
export default function FiveDotsIndicator({
                                            activeIndex = 0,
                                            size = 6,
                                            activeScale = 2.1,
                                            gap = 5,
                                            style = {},
                                          }: DotsIndicatorProps) {
  const dots = new Array(5).fill(null);

  //const colors = ["#cc0000", "#ff9900", "#cccc00", "#66cc00", "#00cc00"];
  const colors = [
    "#ef4444", // red-500
    "#f97316", // orange-500
    "#facc15", // yellow-400
    "#a3e635", // lime-400
    "#4ade80", // green-400
  ];

  return (
    <div
      role="group"
      aria-label={`Five dots indicator, active dot ${activeIndex + 1} of 5`}
      style={{
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        gap: `${gap}px`,
        ...style,
      }}
    >
      {dots.map((_, i) => {
        const isActive = i === activeIndex;
        const dotSize = isActive ? size * activeScale : size;

        return (
          <div
            key={i}
            aria-current={isActive}
            style={{
              width: `${dotSize}px`,
              height: `${dotSize}px`,
              borderRadius: "50%",
              backgroundColor: colors[i],
              transition: "all 0.2s ease-in-out",
              flexShrink: 0,
            }}
          />
        );
      })}
    </div>
  );
}