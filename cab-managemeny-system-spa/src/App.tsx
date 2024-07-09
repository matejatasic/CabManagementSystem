import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Provider } from "react-redux";

import "./App.scss";
import Navbar from "./pages/common/navbar/Navbar";
import CustomerRoutes from "./pages/public/CustomerRoutes";
import AdminRoutes from "./pages/admin/AdminRoutes";
import store from "./pages/common/store";

function App() {
  return (
    <Provider store={store}>
      <BrowserRouter>
        <Navbar />
        <Routes>
          <Route path="/*" element={<CustomerRoutes />}/>
          <Route path="/admin/*" element={<AdminRoutes />} />
        </Routes>
      </BrowserRouter>
    </Provider>
  );
}

export default App;
