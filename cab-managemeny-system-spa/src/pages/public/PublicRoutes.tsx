import { Route, Routes } from "react-router-dom";

import Home from "./home/Home";
import RentCab from "./cab/RentCab";
import Register from "./register/Register";
import Login from "./login/Login";
import Bookings from "./bookings/Bookings";
import ChangeAccountDetails from "./change-account-details/ChangeAccountDetails";
import CabRepository from "../../modules/cab/cab-repository/CabRepository";
import CabGateway from "../../modules/cab/cab-gateway/CabGateway";
import ApiGateway from "../../modules/common/ApiGateway";
import Cab from "../../modules/cab/Cab";

export default function PublicRoutes() {
    return (
      <Routes>
        <Route path="/" element={<Home />}/>
        <Route path="rent-cab" element={<RentCab repository={new CabRepository(new CabGateway(new ApiGateway<Cab>()))} />} />
        <Route path="/register" element={<Register />} />
        <Route path="/login" element={<Login />} />
        <Route path="/bookings" element={<Bookings />} />
        <Route path="/change-account-details" element={<ChangeAccountDetails />} />
      </Routes>
    );
}
