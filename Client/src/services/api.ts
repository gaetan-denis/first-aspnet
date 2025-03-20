import { ApiResponse } from "../types/ApiResponse";
import { Post } from "../types/Post";
import { User } from "../types/User";

export const fetchAllUsers = async() : Promise<ApiResponse<User>> => {
    const response = await fetch("http://localhost:5086/api/v1/users")
    const data = await response.json();
    return data;
}

export const fetchAllPosts = async() : Promise<ApiResponse<Post>> => {
    const response = await fetch('http://localhost:5086/api/v1/posts')
        const data = await response.json();
        return data;
}