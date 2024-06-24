import UserResponseData from "../models/UserResponseDataType";

export default interface IUserGateway {
    getAll(): Promise<UserResponseData[]>;
}