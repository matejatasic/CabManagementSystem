import { BrowserRouter, Route, Routes } from "react-router-dom";

import "./App.scss";
import Navbar from "./pages/common/navbar/Navbar";
import PublicRoutes from "./pages/public/PublicRoutes";
import AdminRoutes from "./pages/admin/AdminRoutes";
import { Provider } from "react-redux";
import store from "./pages/common/store";

function App() {
  return (
    <Provider store={store}>
      <BrowserRouter>
        <Navbar />
        <Routes>
          <Route path="/*" element={<PublicRoutes />}/>
          <Route path="/admin/*" element={<AdminRoutes />} />
        </Routes>
      </BrowserRouter>
    </Provider>
  );
}

export default App;
