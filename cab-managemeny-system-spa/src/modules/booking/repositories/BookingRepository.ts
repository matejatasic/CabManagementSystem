import BookingResponseData from "../types/BookingResponseDataType";
import IBookingGateway from "../gateways/IBookingGateway";
import IBookingRepository from "./IBookingRepository";
import BookingCreateViewModel from "../view-models/BookingCreateViewModel";
import Booking from "../models/Booking";

export default class BookingRepository implements IBookingRepository {
    private readonly bookingGateway: IBookingGateway;

    constructor(bookingGateway: IBookingGateway) {
        this.bookingGateway = bookingGateway;
    }

    public getAll(): Promise<BookingResponseData[]> {
        return this.bookingGateway.getAll();
    }

    public async create(booking: Booking): Promise<BookingResponseData> {
        booking.validate();

        const createViewDto = this.getCreateViewModel(booking);
        const result = await this.bookingGateway.create(createViewDto);

        return result;
    }

    private getCreateViewModel(booking: Booking): BookingCreateViewModel {
        return new BookingCreateViewModel(
            booking.fromAddress,
            booking.toAddress,
            booking.travelerId,
            booking.driverId
        );
    }
}
