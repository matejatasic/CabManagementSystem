import { Route, Routes } from "react-router-dom";
import Home from "./home/Home";
import RentCab from "./cab/RentCab";
import Register from "./register/Register";
import Login from "./login/Login";
import Bookings from "./bookings/Bookings";
import ChangeAccountDetails from "./change-account-details/ChangeAccountDetails";

export default function PublicRoutes() {
    return (
      <Routes>
        <Route path="/" element={<Home />}/>
        <Route path="cabs" element={<RentCab />} />
        <Route path="/register" element={<Register />} />
        <Route path="/login" element={<Login />} />
        <Route path="/bookings" element={<Bookings />} />
        <Route path="/change-account-details" element={<ChangeAccountDetails />} />
      </Routes>
    );
}