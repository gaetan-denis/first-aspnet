import { ApiResponse } from "../types/ApiResponse";

export const fetchAllUsers = async() : Promise<ApiResponse> => {
    const response = await fetch("http://localhost:5086/api/v1/users")
    const data = await response.json();
    return data;
}