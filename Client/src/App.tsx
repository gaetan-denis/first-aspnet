import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import UsersPage from './pages/UserPage'
import PostsPage from './pages/PostPage';
import DashboardPage from './pages/DashboardPage';

function App() {
  return (
    <Router>
      <div>
        <nav>
          <ul>
            <li><a href="/">Dashboard</a></li>
            <li><a href="/users">Users</a></li>
            <li><a href="/posts">Posts</a></li>
          </ul>
        </nav>
        <Routes>
          <Route path="/" element={<DashboardPage />} />
          <Route path="/users" element={<UsersPage />} />
          <Route path="/posts" element={<PostsPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
