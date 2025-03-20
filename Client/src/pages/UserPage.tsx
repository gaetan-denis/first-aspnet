import { useEffect, useState } from "react";
import { ApiResponse } from "../types/ApiResponse";
import { User } from "../types/User";
import { fetchUsers } from "../services/api";

const UserPage: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    fetchUsers().then((response: ApiResponse) => {
      console.log(response);
      setUsers(response.data.data);
    });
  }, []);

  return (
    
    <div>
      <h1>Liste des utilisateurs</h1>
      <ul>
        {users.map((user) => (
          <li key={user.email}>
            <strong>{user.username}</strong> - {user.email}
            {user.isAdmin && <span> (Admin)</span>}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default UserPage;
