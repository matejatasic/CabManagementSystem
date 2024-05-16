import CabResponseData from "../CabResponseDataType";

export default interface ICabGateway {
    getAll(): Promise<CabResponseData[]>;
}
