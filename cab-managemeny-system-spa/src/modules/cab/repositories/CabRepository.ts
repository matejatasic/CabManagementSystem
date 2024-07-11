import CabResponseData from "../types/CabResponseDataType";
import ICabGateway from "../gateways/ICabGateway";
import ICabRepository from "./ICabRepository";

export default class CabRepository implements ICabRepository {
    private readonly cabGateway: ICabGateway;

    constructor(cabGateway: ICabGateway) {
        this.cabGateway = cabGateway;
    }

    public getAll(): Promise<CabResponseData[]> {
        return this.cabGateway.getAll();
    }
}
