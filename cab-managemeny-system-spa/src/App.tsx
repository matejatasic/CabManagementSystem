import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './App.scss';
import Home from './home/Home';
import Navbar from './common/Navbar';

function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        <Route path='/' element={<Home />}/>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
