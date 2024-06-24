import IApiGateway from "../../common/IApiGateway";
import UserResponseData from "../models/UserResponseDataType";
import IUserGateway from "./IUserGateway";

export default class UserGateway implements IUserGateway {
    private readonly apiGateway: IApiGateway;
    private readonly route: string = "users";

    constructor(apiGateway: IApiGateway) {
        this.apiGateway = apiGateway;
    }

    async getAll(): Promise<UserResponseData[]> {
        const result = await this.apiGateway.get(this.route);

        return result.json();
    }
}
