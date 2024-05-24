import { Route, Routes } from "react-router-dom";

import Statistics from "./statistics/Statistics";
import Users from "./users/Users";
import Dashboard from "./common/dashboard/Dashboard";
import Employees from "./employees/Employees";
import Cars from "./cars/Cars";
import Bookings from "./bookings/Bookings";
import BookingRepository from "../../modules/booking/booking-repository/BookingRepository";
import BookingGateway from "../../modules/booking/booking-gateway/BookingGateway";
import ApiGateway from "../../modules/common/ApiGateway";
import UserRepository from "../../modules/user/user-repository/UserRepository";
import UserGateway from "../../modules/user/user-gateway/UserGateway";

export default function AdminRoutes() {
    return (
        <Routes>
            <Route path="/" element={<Dashboard heading="Statistics"><Statistics /></Dashboard>} />
            <Route path="/users" element={<Dashboard heading="Users"><Users repository={new UserRepository(new UserGateway(new ApiGateway()))} /></Dashboard>} />
            <Route path="/employees" element={<Dashboard heading="Employees"><Employees /></Dashboard>} />
            <Route path="/cars" element={<Dashboard heading="Cars"><Cars /></Dashboard>} />
            <Route path="/bookings" element={<Dashboard heading="Bookings"><Bookings repository={new BookingRepository(new BookingGateway(new ApiGateway()))} /></Dashboard>} />
        </Routes>
    );
}
