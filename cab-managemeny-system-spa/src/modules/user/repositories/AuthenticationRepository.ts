import IAuthenticationGateway from "../gateways/IAuthenticationGateway";
import User from "../models/User";
import LoginViewModel from "../view-models/LoginViewModel";
import IAuthenticationRepository from "./IAuthenticationRepository";

export default class AuthenticationRepository implements IAuthenticationRepository {
    private authenticationGateway: IAuthenticationGateway;

    constructor(authenticationGateway: IAuthenticationGateway) {
        this.authenticationGateway = authenticationGateway;
    }

    public async login(user: User): Promise<string> {
        const loginViewModel = this.getLoginViewModel(user);
        const result = await this.authenticationGateway.login(loginViewModel);

        return result;
    }

    public getLoginViewModel(user: User) {
        return new LoginViewModel(user.username, user.password);
    }
}