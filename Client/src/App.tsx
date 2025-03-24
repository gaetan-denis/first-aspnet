import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import UsersPage from './pages/UsersPage'
import PostsPage from './pages/PostsPage';
import HomePage from './pages/HomePage';
import DomainsPage from './pages/DomainsPage';

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
            <li><a href="/domains">Domains</a></li>
          </ul>
        </nav>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/users" element={<UsersPage />} />
          <Route path="/posts" element={<PostsPage />} />
          <Route path="/domains" element={<DomainsPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
