import Booking from "../models/Booking";
import BookingResponseData from "../types/BookingResponseDataType";

export default interface IBookingRepository {
    getAll(): Promise<BookingResponseData[]>;
    create(booking: Booking): Promise<BookingResponseData>;
}
