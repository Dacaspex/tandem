import './UserCard.css';
import Label from "@/components/Label.tsx";

interface UserCardProps {
  userName: string;
  isSelf: boolean;
  lastUpdatedAt: string;
  active?: boolean;
}

export default function UserCard({ userName, isSelf = false, lastUpdatedAt, active = false }: UserCardProps) {
  const isSelfChip = isSelf ? <Label>You</Label> : <Label hidden={ true }/>
  const cardClasses = active ? 'user-card user-card-active' : 'user-card';

  return(
    <div className={ cardClasses }>
      { isSelfChip }
      <div className='user-card-name'>{ userName }</div>
      <div className='user-card-last-updated'>Last updated at { lastUpdatedAt }</div>
    </div>
  );
}