import './Button.css';

interface ButtonProps {
  text: string;
  type?: "button" | "submit" | "reset";
}

export default function Button({ text, type }: ButtonProps) {
  return (
    <button className='button' type={ type ?? "submit" }>{ text }</button>
  )
}