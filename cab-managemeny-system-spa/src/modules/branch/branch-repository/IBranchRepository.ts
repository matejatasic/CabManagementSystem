import BranchResponseData from "../BranchResponseData";

export default interface IBranchRepository {
    getAll(): Promise<BranchResponseData[]>;
}
