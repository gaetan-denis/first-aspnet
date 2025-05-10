import { useEffect, useState } from "react";
import { ApiResponse } from "../types/ApiResponse";
import { Post } from "../types/Post";
import { addAPost, deleteAPost, fetchAllPosts, updateAPost } from "../services/api";

const PostPage: React.FC = () => {
  const [posts, setPosts] = useState<Post[]>([]);
  const [newPost, setNewPost] = useState<Post>({
    postId: 0,
    title: "",
    content: "",
    userId: 0,
  });
  const [isEditing, setIsEditing] = useState<boolean>(false);
  const [editingPost, setEditingPost] = useState<Post | null>(null);

  useEffect(() => {
    fetchAllPosts().then((response: ApiResponse<Post>) => {
      console.log("Response from API:", response);
      setPosts(response.data.data);
    });
  }, []);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setNewPost((prevState) => ({ ...prevState, [name]: value }));
  };

  const handleAddPost = async () => {
    if (!newPost.title || !newPost.content || !newPost.userId) {
      alert("Tous les champs doivent être remplis !");
      return;
    }

    const response = await addAPost(newPost);

    if (response && response.data) {
      setPosts((prevPosts) => [...prevPosts, response.data.data]);
      setNewPost({
        postId: 0,
        title: "",
        content: "",
        userId: 0,
      });
    } else {
      alert("Erreur lors de l'ajout du post");
    }
  };

  const handleDeletePost = async (postId: number) => {
    await deleteAPost(postId);
    setPosts(posts.filter((post) => post.postId !== postId));
  };

  const handleEditPost = (post: Post) => {
    setEditingPost(post);
    setIsEditing(true);
  };

  const handleUpdatePost = async () => {
    if (editingPost) {
      const updatedPostData = await updateAPost(editingPost.postId, editingPost);
      if (updatedPostData) {
        setPosts(posts.map((post) =>
          post.postId === editingPost.postId ? editingPost : post
        ));
        setIsEditing(false);
        setEditingPost(null);
      } else {
        alert("Erreur lors de la mise à jour du post.");
      }
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    if (editingPost) {
      setEditingPost({
        ...editingPost,
        [e.target.name]: e.target.value,
      });
    }
  };

  return (
    <div>
      <h2>Ajouter un post</h2>
      <form onSubmit={handleAddPost}>
        <table>
          <thead>
            <tr>
              <th>User Id</th>
              <th>Titre</th>
              <th>Contenu</th>
              <th>Ajouter</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>
                <input
                  type="number"
                  name="userId"
                  value={newPost.userId}
                  onChange={handleInputChange}
                />
              </td>
              <td>
                <input
                  type="text"
                  name="title"
                  value={newPost.title}
                  onChange={handleInputChange}
                />
              </td>
              <td>
                <textarea
                  name="content"
                  value={newPost.content}
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

      {isEditing && editingPost ? (
        <div>
          <h3>Modifier le post</h3>
          <input
            type="number"
            name="userId"
            value={editingPost.userId}
            onChange={handleChange}
          />
          <input
            type="text"
            name="title"
            value={editingPost.title}
            onChange={handleChange}
          />
          <textarea
            name="content"
            value={editingPost.content}
            onChange={handleChange}
          />
          <button onClick={handleUpdatePost}>Sauvegarder</button>
          <button onClick={() => setIsEditing(false)}>Annuler</button>
        </div>
      ) : (
        <div>
          <h2>Liste des posts</h2>
          <table>
            <thead>
              <tr>
                <th>User Id</th>
                <th>Titre</th>
                <th>Contenu</th>
                <th>Modifier</th>
                <th>Supprimer</th>
              </tr>
            </thead>
            <tbody>
              {posts.map((post) => (
                <tr key={post.postId}>
                  <td>{post.userId}</td>
                  <td>{post.title}</td>
                  <td>{post.content}</td>
                  <td>
                    <button onClick={() => handleEditPost(post)}>
                      <i className="fa-solid fa-pen"></i>
                    </button>
                  </td>
                  <td>
                    <button onClick={() => handleDeletePost(post.postId)}>
                      <i className="fa-solid fa-trash"></i>
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
};

export default PostPage;
