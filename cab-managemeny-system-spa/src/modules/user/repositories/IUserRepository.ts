import UserResponseData from "../models/UserResponseDataType";

export default interface IUserRepository {
    getAll(): Promise<UserResponseData[]>;
}
