import { Route, Routes } from "react-router-dom";

import Home from "./home/Home";
import RentCab from "./rent-cab/RentCab";
import Register from "./register/Register";
import Login from "./login/Login";
import Bookings from "./bookings/Bookings";
import ChangeAccountDetails from "./change-account-details/ChangeAccountDetails";
import CabRepository from "../../modules/cab/cab-repository/CabRepository";
import CabGateway from "../../modules/cab/cab-gateway/CabGateway";
import ApiGateway from "../../modules/common/ApiGateway";
import BookingRepository from "../../modules/booking/booking-repository/BookingRepository";
import BookingGateway from "../../modules/booking/booking-gateway/BookingGateway";
import AuthenticationRepository from "../../modules/user/repositories/AuthenticationRepository";
import AuthenticationGateway from "../../modules/user/gateways/AuthenticationGateway";

export default function PublicRoutes() {
    return (
      <Routes>
        <Route path="/" element={<Home />}/>
        <Route path="rent-cab" element={<RentCab repository={new CabRepository(new CabGateway(new ApiGateway()))} />} />
        <Route path="/register" element={<Register repository={new AuthenticationRepository(new AuthenticationGateway(new ApiGateway()))} />} />
        <Route path="/login" element={<Login repository={new AuthenticationRepository(new AuthenticationGateway(new ApiGateway()))} />} />
        <Route path="/bookings" element={<Bookings repository={new BookingRepository(new BookingGateway(new ApiGateway()))} />} />
        <Route path="/change-account-details" element={<ChangeAccountDetails />} />
      </Routes>
    );
}
