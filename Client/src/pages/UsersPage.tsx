import { useEffect, useState } from "react";
import { ApiResponse } from "../types/ApiResponse";
import { User } from "../types/User";
import { deleteAUser, fetchAllUsers } from "../services/api";

const UserPage: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    fetchAllUsers().then((response: ApiResponse<User>) => {
      console.log('Response from API:', response);
      setUsers(response.data.data);
    });
  }, []);

  const handleDeleteUser = async (userId : number) => {
        await deleteAUser(userId)
        setUsers(users.filter(user => user.userId !== userId));
    }

  

  return (
    <div>
      <h2>Ajouter un utilisateur</h2>
      <thead>
        <tr>
            <th>Nom d'utilisateur</th>
            <th>Adresse mail</th>
            <th>Admin?</th>
            <th>Ajouter</th>
        </tr>
      </thead>
      <tbody>
          <tr>
            <td><input type="text" name="username"/></td>
            <td><input type="email" name="email" /></td>
            <td><input type="checkbox" name="isAdmin"/></td>
            <td><input type="password" name="password" /></td>
            <td><button><i className="fa-solid fa-plus"></i></button></td>
            
            <td></td>
          </tr>
        </tbody>
      <h2>Liste des utilisateurs</h2>
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
            <tr key={user.userId}>
              <td>{user.username}</td>
              <td>{user.email}</td>
              <td>{user.isAdmin ? "Admin" : "Utilisateur"}</td>
              <td><button><i className="fa-solid fa-pen"></i></button></td>
              <td><button onClick={() => handleDeleteUser(user.userId)}><i className="fa-solid fa-trash"></i></button></td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UserPage;
