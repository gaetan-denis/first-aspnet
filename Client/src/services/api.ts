import {User} from "../types/User"

export const fetchUsers = async() : Promise<User[]> => {
    const response = await fetch("http://localhost:5086/api/v1/users")
    const data = await response.json();
    return data;
}