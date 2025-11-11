import './UserCard.css';

interface UserCardProps {
  userName: string;
  isSelf: boolean;
  lastUpdatedAt: string
}

export default function UserCard({ userName, isSelf = false, lastUpdatedAt }: UserCardProps) {
  const isSelfFlare = isSelf
    ? <span className='user-card-self-flare'>You</span>
    : <></>

  return(
    <div className='user-card'>
      <div className='user-card-name'>{ userName }{ isSelfFlare }</div>
      <div className='user-card-last-updated'>Last updated at { lastUpdatedAt }</div>
    </div>
  );
}