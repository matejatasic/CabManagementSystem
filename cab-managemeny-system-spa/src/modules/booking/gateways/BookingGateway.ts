import IApiGateway from "../../common/IApiGateway";
import BookingResponseData from "../types/BookingResponseDataType";
import BookingCreateViewModel from "../view-models/BookingCreateViewModel";
import IBookingGateway from "./IBookingGateway";

export default class BookingGateway implements IBookingGateway {
    private readonly apiGateway: IApiGateway;
    private readonly route: string = "routes";

    constructor(apiGateway: IApiGateway) {
        this.apiGateway = apiGateway;
    }

    async getAll(): Promise<BookingResponseData[]> {
        const result = await this.apiGateway.get(this.route);

        return result.json();
    }

    async create(createViewDto: BookingCreateViewModel): Promise<BookingResponseData> {
        const result = await this.apiGateway.post(this.route, createViewDto);

        return result;
    }
}
