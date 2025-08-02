import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Home from './components/Home';
import TrainersList from './components/TrainersList';
import TrainerDetail from './components/TrainerDetail';
import './App.css';

function App() {
  return (
    <Router>
      <div className="app-container">
        <header>
          <h1>My Academy Trainers App</h1>
          <nav>
            <Link to="/">Home</Link> | <Link to="/trainers">Show Trainers</Link>
          </nav>
        </header>

        <main>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/trainers" element={<TrainersList />} />
            <Route path="/trainers/:id" element={<TrainerDetail />} />
          </Routes>
        </main>
      </div>
    </Router>
  );
}

export default App;