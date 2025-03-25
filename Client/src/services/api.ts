import { ApiResponse } from "../types/ApiResponse";
import { Domain } from "../types/Domain";
import { Post } from "../types/Post";
import { User } from "../types/User";

// Users

export const fetchAllUsers = async (): Promise<ApiResponse<User>> => {
  const response = await fetch("http://localhost:5086/api/v1/users");
  const data = await response.json();
  console.log("User data:", data);
  return data;
};

export const addAUser = async (newUser: User): Promise<ApiResponse<User>> => {
  const response = await fetch("http://localhost:5086/api/v1/users", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(newUser),
  });
  const data = await response.json();
  return data;
};

export const updateAUser = async (userId: number, updatedUser: { name: string, email: string }) => {
  const response = await fetch(`http://localhost:5086/api/v1/users/${userId}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      name: updatedUser.name,
      email: updatedUser.email,
    }),
  });

  const text = await response.text();
  if (text) {
    const data = JSON.parse(text);
    return data;
  }
};

export const deleteAUser = async (userId: number): Promise<void> => {
  const response = await fetch(`http://localhost:5086/api/v1/users/${userId}`, {
    method: "DELETE",
  });
};

// Posts

export const fetchAllPosts = async (): Promise<ApiResponse<Post>> => {
  const response = await fetch("http://localhost:5086/api/v1/posts");
  const data = await response.json();
  return data;
};

export const addAPost = async (newPost: Post): Promise<ApiResponse<Post>> => {
  const response = await fetch("http://localhost:5086/api/v1/posts", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(newPost),
  });
  const data = await response.json();
  return data;
};

export const updateAPost = async (postId: number, updatedPost: { title: string, content: string, userId :  number }) => {
  const response = await fetch(`http://localhost:5086/api/v1/posts/${postId}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      title: updatedPost.title,
      content: updatedPost.content,
      userId : updatedPost.userId,
    }),
  });

  const text = await response.text();
  if (text) {
    const data = JSON.parse(text);
    return data;
  }
};

export const deleteAPost = async (postId: number): Promise<void> => {
  const response = await fetch(`http://localhost:5086/api/v1/posts/${postId}`, {
    method: "DELETE",
  });
};

// Domains

export const fetchAllDomains = async (): Promise<ApiResponse<Domain>> => {
  const response = await fetch("http://localhost:5086/api/v1/domains");
  const data = await response.json();
  console.log(data);
  return data;
};

export const addAdomain = async (newDomain: Domain): Promise<ApiResponse<Domain>> => {
  const response = await fetch("http://localhost:5086/api/v1/domains", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(newDomain),
  });
  const data = await response.json();
  return data;
};

export const updateADomain = async (domainId: number, updatedDomain: { name: string }) => {
  const response = await fetch(`http://localhost:5086/api/v1/domains/${domainId}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      name: updatedDomain.name,
    }),
  });

  const text = await response.text();
  if (text) {
    const data = JSON.parse(text);
    return data;
  }
};


export const deleteADomain = async (domainId: number): Promise<void> => {
  const response = await fetch(
    `http://localhost:5086/api/v1/domains/${domainId}`,
    {
      method: "DELETE",
    }
  );
};
