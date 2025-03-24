import { ApiResponse } from "../types/ApiResponse";
import { Post } from "../types/Post";
import { User } from "../types/User";

// Users

export const fetchAllUsers = async() : Promise<ApiResponse<User>> => {
    const response = await fetch("http://localhost:5086/api/v1/users")
    const data = await response.json();
    console.log("User data:", data); // 🔍 Vérifie ce que tu reçois
    return data;
}

export const deleteUser = async(userID : string) : Promise<void> => {
    const response = await fetch("httpo://localhost:5086/v1/users/{userId}",{
        method : "DELETE",
    });
}



export const fetchAllPosts = async() : Promise<ApiResponse<Post>> => {
    const response = await fetch('http://localhost:5086/api/v1/posts')
        const data = await response.json();
        return data;
}