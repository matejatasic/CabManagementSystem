import IApiGateway from "../../common/IApiGateway";

export default class EmployeeGateway {
    private readonly apiGateway: IApiGateway;
    private readonly route: string = "employees";

    constructor(apiGateway: IApiGateway) {
        this.apiGateway = apiGateway;
    }

    async getAll(): Promise<EmployeeResponseData[]> {
        const result = await this.apiGateway.get(this.route);

        return result.json();
    }
}