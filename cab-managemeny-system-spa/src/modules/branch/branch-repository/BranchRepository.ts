import BranchResponseData from "../BranchResponseData";
import IBranchGateway from "../branch-gateway/IBranchGateway";
import IBranchRepository from "./IBranchRepository";

export default class BranchRepository implements IBranchRepository {
    private readonly branchGateway: IBranchGateway;

    constructor(branchGateway: IBranchGateway) {
        this.branchGateway = branchGateway;
    }

    getAll(): Promise<BranchResponseData[]> {
        return this.branchGateway.getAll();
    }
}