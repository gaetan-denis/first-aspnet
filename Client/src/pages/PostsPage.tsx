import { useEffect, useState } from "react";
import { ApiResponse } from "../types/ApiResponse";
import { Post } from "../types/Post";
import { addAPost, deleteAPost, fetchAllPosts, updateAPost } from "../services/api";

const PostsPage: React.FC = () => {
  const [posts, setPosts] = useState<Post[]>([]);
  const [isEditing, setIsEditing] = useState<boolean>(false);
  const [editingPost, setEditingPost] = useState<Post | null>(null);
  const [newPost, setNewPost] = useState<Post>({
    postId: 0,
    userId: 0,
    title: "",
    content: "",
  });

  useEffect(() => {
    fetchAllPosts().then((response: ApiResponse<Post>) => {
      setPosts(response.data.data);
    });
  }, []);

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
      const updatedPost = {
        title: editingPost.title,
        content: editingPost.content,
        userId: editingPost.userId,
      };

      await updateAPost(editingPost.postId, updatedPost);
      setPosts(
        posts.map((post) =>
          post.postId === editingPost.postId ? { ...editingPost } : post
        )
      );
      setIsEditing(false);
      setEditingPost(null);
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    if (editingPost) {
      setEditingPost({
        ...editingPost,
        [e.target.name]: e.target.value,
      });
    } else {
      setNewPost({
        ...newPost,
        [e.target.name]: e.target.value,
      });
    }
  };

  const handleAddPost = async () => {
    if (!newPost.title || !newPost.content || !newPost.userId) {
      alert("Tous les champs doivent être remplis !");
      return;
    }

    const newPostData = {
      title: newPost.title,
      content: newPost.content,
      userId: newPost.userId,
    };

    const response = await addAPost(newPostData);

    if (response && response.data) {
      setPosts((prevPosts) => [...prevPosts, response.data.data]);
      setNewPost({
        postId: 0,
        userId: 0,
        title: "",
        content: "",
      });
    } else {
      alert("Erreur lors de l'ajout du post");
    }
  };

  return (
    <div>
      <h2>Ajouter un post</h2>
      <form onSubmit={(e) => { e.preventDefault(); handleAddPost(); }}>
        <table>
          <thead>
            <tr>
              <th>Utilisateur Id</th>
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
                  onChange={handleChange}
                />
              </td>
              <td>
                <input
                  type="text"
                  name="title"
                  value={newPost.title}
                  onChange={handleChange}
                />
              </td>
              <td>
                <textarea
                  name="content"
                  value={newPost.content}
                  onChange={handleChange}
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

      <h2>Liste des publications</h2>

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
        <table>
          <thead>
            <tr>
              <th>Utilisateur Id</th>
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
      )}
    </div>
  );
};

export default PostsPage;
