import {StrictMode} from 'react'
import {createRoot} from 'react-dom/client'
import './index.css'
import './App.css'
import {BrowserRouter, Route, Routes} from "react-router";
import Home from "./pages/Home.tsx";
import LoginPage from "./pages/LoginPage.tsx";
import {PrivateRoute} from "./components/AuthenticatedRoute.tsx";
import {AuthProvider} from "./context/AuthProvider.tsx";
import {ModalProvider} from "@/context/Modal/ModalProvider.tsx";
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

const queryClient = new QueryClient();

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <QueryClientProvider client={ queryClient }>
      <BrowserRouter>
        <AuthProvider>
          <ModalProvider>
            <Routes>
              <Route index path="/" element={
                <PrivateRoute>
                  <Home/>
                </PrivateRoute>
              }/>
              <Route path="/login" element={ <LoginPage/> }/>
            </Routes>
          </ModalProvider>
        </AuthProvider>
      </BrowserRouter>
    </QueryClientProvider>
  </StrictMode>
)
