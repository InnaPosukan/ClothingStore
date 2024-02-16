import Header from "./components/Header/Header";
import Footer from "./components/Footer/Footer";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'; // Заменил BrowserRouter на Router
import Shop from './pages/Shop.jsx';
import ShopCategory from './pages/ShopCategory.jsx'
import Auth from "./pages/Auth.jsx";

function App() {
  return (
    <div className="App">
      <Router>
        <Header />
        <Footer />
        <Routes>
          <Route path='/' element={<Shop />} /> 
          <Route path='/mens' element={<ShopCategory  category ="men"/>} /> 
          <Route path='/womens' element={<ShopCategory  category ="women"/>} /> 
          <Route path='/kids' element={<ShopCategory  category ="kid"/>} /> 
          <Route path='/mens' element={<ShopCategory />} /> 
          <Route path='/login' element={<Auth />} /> 

        </Routes>
      </Router>
    </div>
  );
}

export default App;
