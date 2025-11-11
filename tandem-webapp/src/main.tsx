import {StrictMode} from 'react'
import {createRoot} from 'react-dom/client'
import './index.css'
import './App.css'
import {BrowserRouter, Route, Routes} from "react-router";
import UsersListPage from "./pages/UsersListPage.tsx";
import Home from "./pages/Home.tsx";
import UserTopicsPage from "./pages/UserTopicsPage.tsx";
import LoginPage from "./pages/LoginPage.tsx";
import {PrivateRoute} from "./components/AuthenticatedRoute.tsx";
import {AuthProvider} from "./context/AuthProvider.tsx";

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <AuthProvider>
        <Routes>
          <Route index path="/" element={<Home/>}/>
          <Route path="/login" element={<LoginPage/>}/>
          <Route path="/private" element={
            <PrivateRoute>
              <h1>Private</h1>
            </PrivateRoute>
          }/>

          <Route path="users" element={<UsersListPage/>}/>
          <Route path="users/:userId" element={<UserTopicsPage/>}/>
        </Routes>
      </AuthProvider>
    </BrowserRouter>
  </StrictMode>
)
