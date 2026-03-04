import React, {createContext, useContext} from "react";

export enum NotificationTypes {
  success = "success",
  error = "error",
  info = "info",
}

export interface NotificationType {
  id: string;
  message: string;
  type: NotificationTypes;
  durationInMilliseconds?: number;
}

interface ToastNotificationContextProps {
  notifications: NotificationType[];
  addNotification: (notification: NotificationType) => void;
  removeNotification: (id: string) => void;
  CustomComponent?: React.ElementType;
}

export const ToastNotificationContext = createContext<ToastNotificationContextProps | undefined>(
  undefined
);

export const useNotification = (): ToastNotificationContextProps => {
  const context = useContext(ToastNotificationContext);
  if (!context) {
    throw new Error(
      "useNotification must be used within a ToastNotificationProvider"
    );
  }
  return context;
};