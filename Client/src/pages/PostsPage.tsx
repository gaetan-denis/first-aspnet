import { useEffect, useState } from "react";
import { ApiResponse } from "../types/ApiResponse";
import { Post } from "../types/Post";
import { addAPost, deleteAPost, fetchAllPosts } from "../services/api";

const PostsPage: React.FC = () => {
  const [posts, setPosts] = useState<Post[]>([]);
  const [newPost, setNewPost] = useState<Post>({
    postId: 0,
    userId: 0,
    title: "",
    content: "",
  });

  
  useEffect(() => {
    fetchAllPosts().then((response: ApiResponse<Post>) => {
      console.log(response);
      setPosts(response.data.data);
    });
  }, []);

  
  const handleDeletePost = async (postId: number) => {
    await deleteAPost(postId);
    setPosts(posts.filter((post) => post.postId !== postId));
  };

  
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
      setPosts((prevPosts) => [...prevPosts, ...response.data.data]);
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
      <form onSubmit={handleAddPost}>
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

      <h2>Liste des publications</h2>
      <table>
        <thead>
          <tr>
            <th>Id Utilisateur</th>
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
                <button>
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
  );
};

export default PostsPage;
