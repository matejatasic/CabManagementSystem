import CabResponseData from "../types/CabResponseDataType";

export default interface ICabGateway {
    getAll(): Promise<CabResponseData[]>;
}
