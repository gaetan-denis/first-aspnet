import { useEffect, useState } from "react";
import { ApiResponse } from "../types/ApiResponse";
import { Post } from "../types/Post";
import { fetchAllPosts } from "../services/api";

const PostsPage: React.FC = () => {
  const [posts, setPosts] = useState<Post[]>([]);

  useEffect(() => {
    fetchAllPosts().then((response: ApiResponse<Post>) => {
      console.log(response);
      setPosts(response.data.data);
    });
  }, []);
  return (
    <div>
      <h2>Listes des publications</h2>
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
            <tr>
              <td>{post.userId}</td>
              <td>{post.title}</td>
              <td>{post.content}</td>
              <td><button><i className="fa-solid fa-pen"></i></button></td>
              <td><button><i className="fa-solid fa-trash"></i></button></td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default PostsPage;
