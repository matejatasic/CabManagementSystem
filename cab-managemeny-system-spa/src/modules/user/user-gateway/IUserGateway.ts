import UserResponseData from "../UserResponseDataType";

export default interface IUserGateway {
    getAll(): Promise<UserResponseData[]>;
}