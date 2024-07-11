import Booking from "../models/Booking";
import IBookingRepository from "../repositories/IBookingRepository";

type BookACabModalProps = {
    shouldShowModal: boolean,
    handleModalClose: () => void,
    booking: Booking,
    setBooking: (booking: Booking) => void,
    bookingRepository: IBookingRepository
};

export default BookACabModalProps;