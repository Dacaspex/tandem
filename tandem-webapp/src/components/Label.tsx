import './Label.css';
import type {ReactNode} from "react";

interface LabelProps {
  children?: ReactNode,
  hidden?: boolean,
}

export default function Label({ children, hidden = false }: LabelProps) {
  const classes = hidden ? 'label label-hidden' : 'label';

  return (
    <div className={ classes }>{ children }</div>
  )
}