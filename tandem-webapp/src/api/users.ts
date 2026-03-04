import type {UserType} from "../context/AuthContext.tsx";

export interface TopicGroupType {
  id: string;
  name: string;
  topics: TopicType[];
}

export interface TopicType {
  id: string;
  name: string;
  rating: number;
}

export function create(authenticatedFetch: typeof fetch) {
  return {
    async getUser(userId: string): Promise<UserType> {
      const res = await authenticatedFetch("/api/users/" + userId);
      if (!res.ok) throw new Error("Failed to fetch user");
      return res.json();
    },

    async getUsers(): Promise<UserType[]> {
      const res = await authenticatedFetch("/api/users");
      if (!res.ok) throw new Error("Failed to fetch users");
      return res.json();
    },

    async getTopics(userId: string): Promise<TopicGroupType[]> {
      const res = await authenticatedFetch("/api/topics/" + userId);
      if (!res.ok) throw new Error("Failed to fetch topics");
      return res.json();
    },

    async createTopic(topicGroupId: string, topicName: string): Promise<TopicType> {
      const result = await authenticatedFetch("/api/topics", {
        method: "POST",
        body: JSON.stringify({
          topicGroupId: topicGroupId,
          name: topicName
        }),
      });
      if (!result.ok) throw new Error("Failed to create topic");
      return result.json();
    },

    async createTopicGroup(topicGroupName: string): Promise<TopicGroupType> {
      const result = await authenticatedFetch("/api/topicGroups", {
        method: "POST",
        body: JSON.stringify({
          name: topicGroupName,
        })
      });
      if (!result.ok) throw new Error("Failed to create topicGroup");
      return result.json();
    },

    async updateTopicRating(topicId: string, rating: number): Promise<void> {
      const res = await authenticatedFetch("/api/topics", {
        method: "PATCH",
        body: JSON.stringify({
          topicId: topicId,
          rating: rating
        })
      });
      if (!res.ok) throw new Error("Failed to update topic");
    },

    async deleteTopic(topicId: string): Promise<void> {
      const result = await authenticatedFetch("/api/topics", {
        method: "DELETE",
        body: JSON.stringify({
          topicId: topicId,
        })
      });
      if (!result.ok) throw new Error("Failed to delete topic");
    },

    async deleteTopicGroup(topicGroupId: string): Promise<void> {
      const result = await authenticatedFetch("/api/topicGroups", {
        method: "DELETE",
        body: JSON.stringify({
          topicGroupId: topicGroupId,
        })
      });
      if (!result.ok) throw new Error("Failed to delete topicGroup");
    }
  }
}