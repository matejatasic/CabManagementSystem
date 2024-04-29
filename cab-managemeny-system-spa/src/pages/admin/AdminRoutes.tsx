import { Route, Routes } from "react-router-dom";

import Statistics from "./statistics/Statistics";
import Users from "./users/Users";
import Dashboard from "./common/dashboard/Dashboard";

export default function AdminRoutes() {
    return (
        <Routes>
            <Route path="/" element={<Dashboard heading="Statistics"><Statistics /></Dashboard>} />
            <Route path="/users" element={<Dashboard heading="Users"><Users /></Dashboard>} />
        </Routes>
    );
}