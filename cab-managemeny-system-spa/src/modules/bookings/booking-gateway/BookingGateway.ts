import IApiGateway from "../../common/IApiGateway";
import BookingResponseData from "../BookingResponseDataType";
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
}
