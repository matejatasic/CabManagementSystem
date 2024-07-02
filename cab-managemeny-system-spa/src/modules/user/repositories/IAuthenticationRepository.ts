import User from "../models/User";
import AuthenticationResponseData from "../types/AuthenticationResponseDataType";

export default interface IAuthenticationRepository {
    login(user: User): Promise<AuthenticationResponseData>;
    register(user: User): Promise<AuthenticationResponseData>;
};