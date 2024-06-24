import User from "../models/User";

export default interface IAuthenticationRepository {
    login(user: User): Promise<string>;
};