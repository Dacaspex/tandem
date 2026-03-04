import {type ReactNode, useState} from "react";
import {ModalContext} from "@/context/Modal/ModalContext.tsx";
import {createPortal} from "react-dom";
import './Modal.css'

interface ModalProviderProps{
  children: ReactNode;
}

export const ModalProvider = ({ children }: ModalProviderProps) => {
  const [content, setContent] = useState<ReactNode | null>(null);

  const open = (node: ReactNode) => setContent(node);
  const close = () => setContent(null);

  return (
    <ModalContext.Provider value={{ open, close }}>
      { children }

      { content && createPortal(
        <div className='modal'>
          <div className="modal-content">
            { content }
          </div>
        </div>,
        document.body
      )}
    </ModalContext.Provider>
  )
}