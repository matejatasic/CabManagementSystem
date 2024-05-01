import { Route, Routes } from "react-router-dom";

import Statistics from "./statistics/Statistics";
import Users from "./users/Users";
import Dashboard from "./common/dashboard/Dashboard";
import Employees from "./employees/Employees";
import Cars from "./cars/Cars";

export default function AdminRoutes() {
    return (
        <Routes>
            <Route path="/" element={<Dashboard heading="Statistics"><Statistics /></Dashboard>} />
            <Route path="/users" element={<Dashboard heading="Users"><Users /></Dashboard>} />
            <Route path="/employees" element={<Dashboard heading="Employees"><Employees /></Dashboard>} />
            <Route path="/cars" element={<Dashboard heading="Cars"><Cars /></Dashboard>} />
        </Routes>
    );
}