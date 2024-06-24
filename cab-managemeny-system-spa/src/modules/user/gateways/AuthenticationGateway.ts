import IApiGateway from "../../common/IApiGateway";
import LoginViewModel from "../view-models/LoginViewModel";
import IAuthenticationGateway from "./IAuthenticationGateway";

export default class AuthenticationGateway implements IAuthenticationGateway {
    private readonly apiGateway: IApiGateway;
    private readonly route: string = "login";

    constructor(apiGateway: IApiGateway) {
        this.apiGateway = apiGateway;
    }

    public async login(loginViewModel: LoginViewModel): Promise<string> {
        console.log(loginViewModel);
        const result = await this.apiGateway.post(this.route, loginViewModel);

        return result.json();
    }
}