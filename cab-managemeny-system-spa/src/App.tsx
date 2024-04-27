import { BrowserRouter, Route, Routes } from "react-router-dom";

import "./App.scss";
import Home from "./pages/home/Home";
import Navbar from "./common/navbar/Navbar";
import RentCab from "./pages/cab/RentCab";
import Register from "./pages/register/Register";
import Login from "./pages/login/Login";
import Bookings from "./pages/bookings/Bookings";
import ChangeAccountDetails from "./pages/change-account-details/ChangeAccountDetails";

function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />}/>
        <Route path="/cabs" element={<RentCab />} />
        <Route path="/register" element={<Register />} />
        <Route path="/login" element={<Login />} />
        <Route path="/bookings" element={<Bookings />} />
        <Route path="/change-account-details" element={<ChangeAccountDetails />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
