import Header from "./components/Header/Header";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'; // Заменил BrowserRouter на Router
import Shop from './pages/Shop.jsx';
import ShopCategory from './pages/ShopCategory.jsx'
import Auth from "./pages/Auth.jsx";
import CreateAdvertisement from "./pages/CreateAdvertisement.jsx";
function App() {
  return (
    <div className="App">
      <Router>
        <Header />
        <Routes>
          <Route path='/' element={<Shop />} /> 
          <Route path='/men' element={<ShopCategory  category ="men"/>} /> 
          <Route path='/women' element={<ShopCategory  category ="women"/>} /> 
          <Route path='/kids' element={<ShopCategory  category ="kid"/>} /> 
          <Route path='/create' element={<CreateAdvertisement/>} /> 
          <Route path='/login' element={<Auth/>} /> 
        </Routes>
      </Router>
    </div>
  );
}

export default App;
