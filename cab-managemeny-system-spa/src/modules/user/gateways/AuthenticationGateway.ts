import IApiGateway from "../../common/IApiGateway";
import LoginViewModel from "../view-models/LoginViewModel";
import RegisterViewModel from "../view-models/RegisterViewModel";
import IAuthenticationGateway from "./IAuthenticationGateway";

export default class AuthenticationGateway implements IAuthenticationGateway {
    private readonly apiGateway: IApiGateway;
    private readonly loginRoute: string = "login";
    private readonly registerRoute: string = "register";

    constructor(apiGateway: IApiGateway) {
        this.apiGateway = apiGateway;
    }

    public async login(loginViewModel: LoginViewModel): Promise<string> {
        const result = await this.apiGateway.post(this.loginRoute, loginViewModel);

        return result.json();
    }

    public async register(registerViewModel: RegisterViewModel): Promise<string> {
        const result = await this.apiGateway.post(this.registerRoute, registerViewModel);

        return result.json();
    }
}