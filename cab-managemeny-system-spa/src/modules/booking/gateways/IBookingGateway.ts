import BookingResponseData from "../types/BookingResponseDataType";
import BookingCreateViewModel from "../view-models/BookingCreateViewModel";

export default interface IBookingGateway {
    getAll(): Promise<BookingResponseData[]>;
    create(createViewModel: BookingCreateViewModel): Promise<BookingResponseData>;
}
