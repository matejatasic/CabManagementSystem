import { useEffect, useState } from "react";

import { ArrowUp } from "react-bootstrap-icons";

import "./Bookings.scss"
import ContentCard from "../common/components/content-card/ContentCard";
import PageProps from "../../common/props/PageProps";
import IBookingRepository from "../../../modules/booking/repositories/IBookingRepository";
import Booking from "../../../modules/booking/models/Booking";

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
            })
            .catch((error: Error) => {
                console.log(error);
            })
    }, []);

    const headers = ["Date Booked", "From Address", "To Address", "Status"];

    return (
        <div id="bookings-background">
            <main className="container d-flex justify-content-center align-items-center">
                <ContentCard heading="Your Bookings">
                    <>
                        <div className="row">
                            <div className="col-12 table-responsive">
                                <table className="table mt-5 mb-4">
                                    <thead>
                                        <tr>
                                            {headers.map((header, index) => (
                                                <th key={index}>{ header } <span className="sort-arrow"><ArrowUp /></span></th>
                                            ))}
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {bookings?.map(booking => (
                                            <tr key={booking.id}>
                                                <td>2023-04-03</td>
                                                <td>{booking.fromAddress}</td>
                                                <td>{booking.toAddress}</td>
                                                <td><span className="badge bg-primary">Pending</span></td>
                                                <td><button className="btn btn-dark">View Details</button></td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-0 col-md-4 col-xl-7"></div>
                            <div className="col-5 col-md-4 col-xl-2">
                                <span>Show: </span> <input type="number" className="d-inline-block w-25 mx-2" /> entries
                            </div>
                            <div className="col-7 col-md-4 col-xl-3">
                                <ul className="pagination">
                                    <li className="page-item">
                                        <a className="page-link" href="#" aria-label="Previous">
                                            <span aria-hidden="true">&laquo;</span>
                                        </a>
                                    </li>
                                    <li className="page-item"><a className="page-link" href="#">1</a></li>
                                    <li className="page-item"><a className="page-link" href="#">2</a></li>
                                    <li className="page-item"><a className="page-link" href="#">3</a></li>
                                    <li className="page-item">
                                        <a className="page-link" href="#" aria-label="Next">
                                            <span aria-hidden="true">&raquo;</span>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </>
                </ContentCard>
            </main>
        </div>
    );
}