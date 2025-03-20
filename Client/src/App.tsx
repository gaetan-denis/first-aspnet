import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import UsersPage from './pages/UserPage'
import PostsPage from './pages/PostPage';
import HomePage from './pages/HomePage';

function App() {
  return (
    <Router>
      <div>
        <h1>Panneau D'administration</h1>
        <nav>
          <ul>
            <li><a href='/'>Home</a></li>
            <li><a href="/users">Users</a></li>
            <li><a href="/posts">Posts</a></li>
          </ul>
        </nav>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/users" element={<UsersPage />} />
          <Route path="/posts" element={<PostsPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
