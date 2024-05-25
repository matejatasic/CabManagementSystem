import IApiGateway from "../../common/IApiGateway";
import BranchResponseData from "../BranchResponseData";
import IBranchGateway from "./IBranchGateway";

export default class BranchGateway implements IBranchGateway {
    private readonly apiGateway: IApiGateway;
    private readonly route: string = "branches";

    constructor(apiGateway: IApiGateway) {
        this.apiGateway = apiGateway;
    }

    async getAll(): Promise<BranchResponseData[]> {
        const result = await this.apiGateway.get(this.route);

        return result.json();
    }
}