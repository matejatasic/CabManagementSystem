import IEmployeeGateway from "../employee-gateway/IEmployeeGateway";

export default class EmployeeRepository {
    private readonly userGateway: IEmployeeGateway;

    constructor(userGateway: IEmployeeGateway) {
        this.userGateway = userGateway;
    }

    getAll(): Promise<EmployeeResponseData[]> {
        return this.userGateway.getAll();
    }
}