import type {UserType} from "../context/AuthContext.tsx";

export interface TopicGroup {
  id: string;
  name: string;
  topics: Topic[];
}

export interface Topic {
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

    async getTopics(userId: string): Promise<TopicGroup[]> {
      const res = await authenticatedFetch("/api/topics/" + userId);
      if (!res.ok) throw new Error("Failed to fetch topics");
      return res.json();
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
    }
  }
}