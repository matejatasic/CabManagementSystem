import AuthenticationResponseData from "../types/AuthenticationResponseDataType";
import LoginViewModel from "../view-models/LoginViewModel";
import RegisterViewModel from "../view-models/RegisterViewModel";

export default interface IAuthenticationGateway {
    login(loginViewModel: LoginViewModel): Promise<AuthenticationResponseData>;
    register(registerViewModel: RegisterViewModel): Promise<AuthenticationResponseData>;
}