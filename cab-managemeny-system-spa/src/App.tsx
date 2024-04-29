import { BrowserRouter, Route, Routes } from "react-router-dom";

import "./App.scss";
import Navbar from "./common/navbar/Navbar";
import PublicRoutes from "./pages/public/PublicRoutes";
import AdminRoutes from "./pages/admin/AdminRoutes";

function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        <Route path="/*" element={<PublicRoutes />}/>
        <Route path="/admin/*" element={<AdminRoutes />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
