import BookingResponseData from "../BookingResponseDataType";
import IBookingGateway from "../booking-gateway/IBookingGateway";
import IBookingRepository from "./IBookingRepository";

export default class BookingRepository implements IBookingRepository {
    private readonly bookingGateway: IBookingGateway;

    constructor(bookingGateway: IBookingGateway) {
        this.bookingGateway = bookingGateway;
    }

    public getAll(): Promise<BookingResponseData[]> {
        return this.bookingGateway.getAll();
    }
}
