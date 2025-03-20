import { useEffect, useState } from "react";
import { ApiResponse } from "../types/ApiResponse";
import { User } from "../types/User";
import { fetchAllUsers } from "../services/api";

const UserPage: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    fetchAllUsers().then((response: ApiResponse<User>) => {
      setUsers(response.data.data);
    });
  }, []);

  return (
    <div>
      <table>
        <thead>
          <tr>
            <th>Nom d'utilisateur</th>
            <th>Adresse mail</th>
            <th>Statut</th>
            <th>Modifier</th>
            <th>Supprimer</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr>
              <td>{user.username}</td>
              <td>{user.email}</td>
              <td>{user.isAdmin ? "Admin" : "Utilisateur"}</td>
              <td><button><i className="fa-solid fa-pen"></i></button></td>
              <td><button><i className="fa-solid fa-trash"></i></button></td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UserPage;
