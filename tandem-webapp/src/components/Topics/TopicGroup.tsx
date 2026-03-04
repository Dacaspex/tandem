import "./Topic.css";
import type {TopicType as TopicType} from "../../api/users.ts";
import {useApi} from "@/api/client.ts";
import Topic from "@/components/Topics/Topic.tsx";
import React from "react";
import {useModal} from "@/context/Modal/ModalContext.tsx";
import CreateTopicModal from "@/Modals/CreateTopicModal.tsx";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import {LuPencil, LuTrash} from "react-icons/lu";

interface TopicGroupProps {
  id: string;
  title: string;
  topics: TopicType[];
  editMode: boolean;
  canChangeRating: boolean;
  userId: string;
}

interface UpdateTopicRating {
  topicId: string;
  rating: number;
}

/**
 * TopicGroup
 * - Displays a titled box with a list of TopicOld components
 */
export default function TopicGroup({ id, title, topics, editMode, canChangeRating, userId }: TopicGroupProps) {
  const api = useApi();
  const { open, close } = useModal();
  const queryClient = useQueryClient();

  const createTopicMutation = useMutation({
    mutationFn: (name: string) => {
      return api.users.createTopic(id, name);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["topicGroups", userId],
      });
    },
  });

  const updateTopicMutation = useMutation({
    mutationFn: ({topicId, rating}: UpdateTopicRating) => {
      return api.users.updateTopicRating(topicId, rating);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["topicGroups", userId],
      });
    }
  });

  const deleteTopicGroupMutation = useMutation({
    mutationFn: () => {
      return api.users.deleteTopicGroup(id);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["topicGroups", userId],
      })
    }
  });

  const onTopicRatingUpdated = async (topic: TopicType, rating: number) => {
    await updateTopicMutation.mutateAsync({topicId: topic.id, rating});
  };

  const createTopic = async (topicName: string) => {
    close();
    await createTopicMutation.mutateAsync(topicName);
  };

  const onAddTopic = () => {
    open(<CreateTopicModal onSubmit={ name => createTopic(name) } onCancel={ close }/>);
  };

  const deleteTopicGroup = async () => {
    await deleteTopicGroupMutation.mutateAsync();
  }

  const addTopicButton = (
    <div
      className="button button-primary button-outlined button-s"
      onClick={() => { onAddTopic() }}>
      Add topic
    </div>
  )

  const editTopicGroupJsx = (
    <div className="topic-group-edit">
      <LuPencil/>
      <div onClick={ deleteTopicGroup }>
        <LuTrash className='text-danger'/>
      </div>
    </div>
  );

  return (
    <div className="topic-group-container mb-1">
      <div className="topic-group-title">
        { title }
      </div>
      { editMode && editTopicGroupJsx }
      <div className="topic-group-box">
        {topics.map((topic) => (
          <Topic
            key={ topic.id }
            id={ topic.id }
            name={ topic.name }
            initialRating={ topic.rating }
            userId={ userId }
            editMode={ editMode }
            canChangeRating={ canChangeRating }
            onRatingUpdated={ newRating => onTopicRatingUpdated(topic, newRating) }/>
        ))}
        { editMode && addTopicButton }
      </div>
    </div>
  );
}