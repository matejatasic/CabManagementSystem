import UserResponseData from "../types/UserResponseDataType";
import IUserGateway from "../gateways/IUserGateway";
import IUserRepository from "./IUserRepository";

export default class UserRepository implements IUserRepository {
    private readonly userGateway: IUserGateway;

    constructor(userGateway: IUserGateway) {
        this.userGateway = userGateway;
    }

    getAll(): Promise<UserResponseData[]> {
        return this.userGateway.getAll();
    }
}
