import { useEffect, useState } from "react";
import { ApiResponse } from "../types/ApiResponse";
import { User } from "../types/User";
import { addAUser, deleteAUser, fetchAllUsers } from "../services/api";

const UserPage: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [newUser, setNewUser] = useState<User>({
    userId: 0,
    username: "",
    email: "",
    password: "",
    isAdmin: false,
  });

  useEffect(() => {
    fetchAllUsers().then((response: ApiResponse<User>) => {
      console.log("Response from API:", response);
      setUsers(response.data.data);
    });
  }, []);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value, type, checked } = e.target;

    if (type === "checkbox") {
      setNewUser((prevState) => ({ ...prevState, [name]: checked }));
    } else {
      setNewUser((prevState) => ({ ...prevState, [name]: value }));
    }
  };

  const handleAddUser = async () => {
    if (!newUser.username || !newUser.email || !newUser.password) {
      alert("Tous les champs doivent être remplis !");
      return;
    }
  
    const response = await addAUser(newUser);
  
    if (response && response.data) {

      setUsers([...users, response.data]);
      setNewUser({
        userId: 0,
        username: '',
        email: '',
        password: '',
        isAdmin: false
      });  // Réinitialise le formulaire
    } else {
      alert("Erreur lors de l'ajout de l'utilisateur");
    }
  };

  const handleDeleteUser = async (userId: number) => {
    await deleteAUser(userId);
    setUsers(users.filter((user) => user.userId !== userId));
  };

  return (
    <div>
      <h2>Ajouter un utilisateur</h2>
      <thead>
        <tr>
          <th>Nom d'utilisateur</th>
          <th>Adresse mail</th>
          <th>Password</th>
          <th>Admin</th>
          <th>Ajouter</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>
            <input
              type="text"
              name="username"
              value={newUser.username}
              onChange={handleInputChange}
            />
          </td>
          <td>
            <input
              type="email"
              name="email"
              value={newUser.email}
              onChange={handleInputChange}
            />
          </td>
          <td>
            <input
              type="password"
              name="password"
              value={newUser.password}
              onChange={handleInputChange}
            />
          </td>
          <td>
            <input
              type="checkbox"
              name="isAdmin"
              checked={newUser.isAdmin}
              onChange={handleInputChange}
            />
          </td>
       
          <td>
            <button onClick={handleAddUser}>
              <i className="fa-solid fa-plus"></i>
            </button>
          </td>

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
              <td>
                <button>
                  <i className="fa-solid fa-pen"></i>
                </button>
              </td>
              <td>
                <button onClick={() => handleDeleteUser(user.userId)}>
                  <i className="fa-solid fa-trash"></i>
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UserPage;
