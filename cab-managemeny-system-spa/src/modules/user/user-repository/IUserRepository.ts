import UserResponseData from "../UserResponseDataType";

export default interface IUserRepository {
    getAll(): Promise<UserResponseData[]>;
}
