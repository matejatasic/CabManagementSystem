import IBookingRepository from "../../../modules/booking/repositories/IBookingRepository";
import ICabRepository from "../../../modules/cab/repositories/ICabRepository";
import PageProps from "../../common/props/PageProps";

type RentCabProps = PageProps<ICabRepository> & {
    bookingRepository: IBookingRepository
};

export default RentCabProps;