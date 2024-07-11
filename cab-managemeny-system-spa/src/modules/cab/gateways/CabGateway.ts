import IApiGateway from "../../common/IApiGateway";
import CabResponseData from "../types/CabResponseDataType";
import ICabGateway from "./ICabGateway";

export default class CabGateway implements ICabGateway {
    private readonly apiGateway: IApiGateway;
    private readonly route: string = "cars";

    constructor(apiGateway: IApiGateway) {
        this.apiGateway = apiGateway;
    }

    async getAll(): Promise<CabResponseData[]> {
        const result = await this.apiGateway.get(this.route);

        return result.json();
    }
}
