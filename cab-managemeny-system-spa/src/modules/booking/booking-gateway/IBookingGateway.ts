import BookingResponseData from "../BookingResponseDataType";

export default interface IBookingGateway {
    getAll(): Promise<BookingResponseData[]>;
}
