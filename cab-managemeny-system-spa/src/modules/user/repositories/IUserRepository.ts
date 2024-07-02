import UserResponseData from "../types/UserResponseDataType";

export default interface IUserRepository {
    getAll(): Promise<UserResponseData[]>;
}
