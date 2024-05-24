import BookingResponseData from "../BookingResponseDataType";

export default interface IBookingRepository {
    getAll(): Promise<BookingResponseData[]>;
}
