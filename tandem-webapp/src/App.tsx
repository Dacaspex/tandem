import './App.css'
import UserCard from "./components/UserCard.tsx";

function App() {
  return (
    <>
      <h1>Tandem</h1>
      <UserCard userName='Casper' isSelf={ true } lastUpdatedAt='yesterday'/>
      <div style={{ padding: '7px' }}/>
      <UserCard userName='Tom' isSelf={ false } lastUpdatedAt='today'/>
    </>
  )
}

export default App
