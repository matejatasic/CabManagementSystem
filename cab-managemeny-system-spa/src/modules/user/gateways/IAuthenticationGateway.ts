import LoginViewModel from "../view-models/LoginViewModel";

export default interface IAuthenticationGateway {
    login(loginViewModel: LoginViewModel): Promise<string>;
}