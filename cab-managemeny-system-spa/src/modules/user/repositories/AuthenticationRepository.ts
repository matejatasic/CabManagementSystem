import IAuthenticationGateway from "../gateways/IAuthenticationGateway";
import User from "../models/User";
import AuthenticationResponseData from "../types/AuthenticationResponseDataType";
import LoginViewModel from "../view-models/LoginViewModel";
import RegisterViewModel from "../view-models/RegisterViewModel";
import IAuthenticationRepository from "./IAuthenticationRepository";

export default class AuthenticationRepository implements IAuthenticationRepository {
    private authenticationGateway: IAuthenticationGateway;

    constructor(authenticationGateway: IAuthenticationGateway) {
        this.authenticationGateway = authenticationGateway;
    }

    public async login(user: User): Promise<AuthenticationResponseData> {
        const loginViewModel = this.getLoginViewModel(user);
        const result = await this.authenticationGateway.login(loginViewModel);

        return result;
    }

    private getLoginViewModel(user: User): LoginViewModel {
        return new LoginViewModel(user.username, user.password);
    }

    public async register(user: User): Promise<AuthenticationResponseData> {
        const registerViewModel = this.getRegisterViewModel(user);
        const result = await this.authenticationGateway.register(registerViewModel);

        return result;
    }

    private getRegisterViewModel(user: User): RegisterViewModel {
        return new RegisterViewModel(
            user.username,
            user.password,
            user.email,
            user.firstName,
            user.lastName,
            user.address,
            user.phone
        );
    }
}