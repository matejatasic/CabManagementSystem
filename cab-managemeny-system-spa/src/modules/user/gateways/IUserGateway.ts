import UserResponseData from "../types/UserResponseDataType";

export default interface IUserGateway {
    getAll(): Promise<UserResponseData[]>;
}