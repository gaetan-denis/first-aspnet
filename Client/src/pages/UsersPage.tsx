import { useEffect, useState } from "react";
import { ApiResponse } from "../types/ApiResponse";
import { User } from "../types/User";
import { addAUser, deleteAUser, fetchAllUsers, updateAUser } from "../services/api";

const UserPage: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [newUser, setNewUser] = useState<User>({
    userId: 0,
    username: "",
    email: "",
    password: "",
    isAdmin: false,
  });

  const [isEditing, setIsEditing] = useState<number | null>(null);
  const [editingUser, setEditingUser] = useState<User | null>(null);

  useEffect(() => {
    fetchAllUsers().then((response: ApiResponse<User>) => {
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

  const handleAddUser = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!newUser.username || !newUser.email || !newUser.password) {
      alert("Tous les champs doivent être remplis !");
      return;
    }

    const response = await addAUser(newUser);

    if (response?.data) {
      setUsers((prevUsers) => [...prevUsers, response.data]);
      setNewUser({
        userId: 0,
        username: "",
        email: "",
        password: "",
        isAdmin: false,
      });
    } else {
      alert("Erreur lors de l'ajout de l'utilisateur");
    }
  };

  const handleDeleteUser = async (userId: number) => {
    await deleteAUser(userId);
    setUsers(users.filter((user) => user.userId !== userId));
  };

  const handleEditUser = (user: User) => {
    setIsEditing(user.userId);
    setEditingUser({ ...user });
  };

  const handleEditInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (editingUser) {
      const { name, value, type, checked } = e.target;
      setEditingUser({
        ...editingUser,
        [name]: type === "checkbox" ? checked : value,
      });
    }
  };

  const handleUpdateUser = async () => {
    if (editingUser) {
      const updatedUserData = await updateAUser(editingUser.userId, editingUser);
      console.log(updatedUserData);
      if (updatedUserData) {
        setUsers(
          users.map((user) =>
            user.userId === editingUser.userId ? editingUser : user
          )
        );
        setIsEditing(null);
        setEditingUser(null);
      } else {
        alert("Erreur lors de la mise à jour de l'utilisateur.");
      }
    }
  };

  return (
    <div>
      <h2>Ajouter un utilisateur</h2>
      <form onSubmit={handleAddUser}>
        <table>
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
                <button type="submit">
                  <i className="fa-solid fa-plus"></i>
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </form>

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
              {isEditing === user.userId && editingUser ? (
                <>
                  <td>
                    <input
                      type="text"
                      name="username"
                      value={editingUser.username}
                      onChange={handleEditInputChange}
                    />
                  </td>
                  <td>
                    <input
                      type="email"
                      name="email"
                      value={editingUser.email}
                      onChange={handleEditInputChange}
                    />
                  </td>
                  <td>••••••••</td>
                  <td>
                    <input
                      type="checkbox"
                      name="isAdmin"
                      checked={editingUser.isAdmin}
                      onChange={handleEditInputChange}
                    />
                  </td>
                  <td>
                    <button onClick={handleUpdateUser}>
                      <i className="fa-solid fa-save"></i>
                    </button>
                    <button onClick={() => setIsEditing(null)}>
                      <i className="fa-solid fa-xmark"></i>
                    </button>
                  </td>
                </>
              ) : (
                <>
                  <td>{user.username}</td>
                  <td>{user.email}</td>
                  <td>{user.isAdmin ? "Admin" : "Utilisateur"}</td>
                  <td>
                    <button onClick={() => handleEditUser(user)}>
                      <i className="fa-solid fa-pen"></i>
                    </button>
                  </td>
                  <td>
                    <button onClick={() => handleDeleteUser(user.userId)}>
                      <i className="fa-solid fa-trash"></i>
                    </button>
                  </td>
                </>
              )}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UserPage;
