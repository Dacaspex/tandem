import {createContext, type ReactNode, useContext} from "react";

interface ModalContextProps {
  open: (content: ReactNode) => void;
  close: () => void;
}

export const ModalContext = createContext<ModalContextProps | null>(null);

export const useModal = (): ModalContextProps => {
  const context = useContext(ModalContext);
  if (!context) {
    throw new Error('useModal must be used within ModalContext');
  }
  return context;
}