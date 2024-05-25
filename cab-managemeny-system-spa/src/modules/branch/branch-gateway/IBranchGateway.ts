import BranchResponseData from "../BranchResponseData";

export default interface IBranchGateway {
    getAll(): Promise<BranchResponseData[]>;
}