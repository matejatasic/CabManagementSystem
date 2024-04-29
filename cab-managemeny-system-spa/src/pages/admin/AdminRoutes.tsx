import { Route, Routes } from "react-router-dom";
import Statistics from "./statistics/Statistics";

export default function AdminRoutes() {
    return (
        <Routes>
            <Route path="/" element={<Statistics />} />
        </Routes>
    );
}