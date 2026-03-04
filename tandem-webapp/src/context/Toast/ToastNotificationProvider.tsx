import React, {type ReactNode, useEffect, useState} from "react";
import {type NotificationType, ToastNotificationContext} from "@/context/Toast/ToastNotificationContext.tsx";

interface ToastNotificationProviderProps {
  children: ReactNode;
  customComponent?: React.ElementType;
}

export const ToastNotificationProvider = ({
  children,
  customComponent: CustomComponent
}: ToastNotificationProviderProps) => {
  const [notifications, setNotifications] = useState<NotificationType[]>([]);

  const addNotification = (notification: NotificationType) => {
    setNotifications(oldNotifications => [
      ...oldNotifications,
      notification
    ]);
  }

  const removeNotification = (id: string) => {
    setNotifications(oldNotifications =>
      oldNotifications.filter(n => n.id !== id)
    );
  }

  useEffect(() => {
    notifications.forEach(notification => {
      // Automatically dismiss the notification if the duration has been set
      if (!notification.durationInMilliseconds) return;

      const timeoutId = setTimeout(
        () => { removeNotification(notification.id);},
        notification.durationInMilliseconds
      );

      return () => clearTimeout(timeoutId);
    });
  }, [notifications]);

  return (
    <ToastNotificationContext.Provider
      value={{
        notifications,
        addNotification,
        removeNotification,
        CustomComponent
      }}
    >
      { children }
    </ToastNotificationContext.Provider>
  )
}