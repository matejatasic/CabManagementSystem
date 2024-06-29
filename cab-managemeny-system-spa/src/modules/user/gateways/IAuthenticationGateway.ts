import LoginViewModel from "../view-models/LoginViewModel";
import RegisterViewModel from "../view-models/RegisterViewModel";

export default interface IAuthenticationGateway {
    login(loginViewModel: LoginViewModel): Promise<string>;
    register(registerViewModel: RegisterViewModel): Promise<string>;
}