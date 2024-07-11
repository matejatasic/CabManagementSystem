import CabResponseData from "../types/CabResponseDataType";

export default interface ICabRepository {
    getAll(): Promise<CabResponseData[]>;
};
