import CabResponseData from "../CabResponseDataType";

export default interface ICabRepository {
    getAll(): Promise<CabResponseData[]>;
};
