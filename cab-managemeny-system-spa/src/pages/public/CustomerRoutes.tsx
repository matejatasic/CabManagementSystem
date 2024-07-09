import { Route, Routes } from "react-router-dom";

import Home from "./home/Home";
import Register from "./register/Register";
import Login from "./login/Login";
import ApiGateway from "../../modules/common/ApiGateway";
import AuthenticationRepository from "../../modules/user/repositories/AuthenticationRepository";
import AuthenticationGateway from "../../modules/user/gateways/AuthenticationGateway";
import ProtectedRoutes from "./ProtectedRoutes";
import CustomerRoutesEnum from "./common/enums/CustomerRoutesEnum";

export default function CustomerRoutes() {
    return (
      <Routes>
        <Route path={CustomerRoutesEnum.Home} element={<Home />}/>
        <Route path="/*" element={<ProtectedRoutes />} />
        <Route
          path={CustomerRoutesEnum.Register}
          element={
            <Register
              repository={new AuthenticationRepository(new AuthenticationGateway(new ApiGateway()))}
            />
          }
        />
        <Route
          path={CustomerRoutesEnum.Login}
          element={
            <Login
              repository={new AuthenticationRepository(new AuthenticationGateway(new ApiGateway()))}
            />
          }
        />
      </Routes>
    );
}
