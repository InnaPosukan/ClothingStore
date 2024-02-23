import Header from "./components/Header/Header";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Shop from './pages/Shop.jsx';
import ShopCategory from './pages/ShopCategory.jsx'
import Auth from "./pages/Auth.jsx";
import CreateAdvertisement from "./pages/CreateAdvertisement.jsx";
import AdvertisementPage from "./pages/AdvertisementPage.jsx";
import OrderPage from "./pages/OrderPage.jsx";
import Profile from "./pages/Profile.jsx";
import OrderHistory from "./pages/OrderHistory.jsx";
import MyOrders from "./pages/MyOrders.jsx";
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
          <Route path='/all' element={<ShopCategory  category ="all"/>} /> 
          <Route path='/create' element={<CreateAdvertisement/>} /> 
          <Route path='/order' element={<OrderPage/>} /> 
          <Route path='/profile' element={<Profile/>} /> 
          <Route path="/advertisement/:id" element={<AdvertisementPage />} />
          <Route path='/login' element={<Auth/>} /> 
          <Route path='/history' element={<OrderHistory/>} /> 
          <Route path='/my' element={<MyOrders/>} /> 


        </Routes>
      </Router>
    </div>
  );
}

export default App;
