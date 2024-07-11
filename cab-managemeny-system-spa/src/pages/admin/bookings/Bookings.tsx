import { useEffect, useState } from "react";

import PageProps from "../../common/props/PageProps";
import ContentCard from "../common/content-card/ContentCard";
import TableContent from "../common/table-content/TableContent";
import Booking from "../../../modules/booking/models/Booking";
import IBookingRepository from "../../../modules/booking/repositories/IBookingRepository";

export default function Bookings(props: PageProps<IBookingRepository>) {
    const { repository } = props;
    const [bookings, setBookings] = useState<Booking[]>();

    useEffect(() => {
        repository.getAll()
            .then(data => {
                setBookings(data.map(booking => new Booking(
                    booking.id,
                    booking.fromAddress,
                    booking.toAddress,
                    booking.travelCost
                )));
            });
    }, []);

    const headers = ["Date Booked", "From", "To", "Status"];
    const rows = bookings ? bookings.map(booking => {
        return [
            "2023-04-03",
            booking.fromAddress,
            booking.toAddress,
            "Pending"
        ];
    }) : [];

    return (
        <ContentCard>
            <TableContent headers={headers} rows={rows} />
        </ContentCard>
    );
}
