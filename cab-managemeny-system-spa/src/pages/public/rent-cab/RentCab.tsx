import { useEffect, useState } from "react";

import Hero from "../../common/hero/Hero";
import image from "../../../assets/images/rent-cab-hero.jpg";
import Footer from "../../common/footer/Footer";
import ContentCard from "../common/components/content-card/ContentCard";
import Cab from "../../../modules/cab/models/Cab";
import CabCard from "../../../modules/cab/components/CabCard";
import RentCabProps from "./RentCabProps";
import Booking from "../../../modules/booking/models/Booking";
import { useSelector } from "react-redux";
import RootState from "../../common/store/state.type";
import BookACabModal from "../../../modules/booking/components/BookACabModal";

export default function RentCab(props: RentCabProps) {
    const { repository, bookingRepository } = props;

    const user = useSelector((state: RootState) => state.user);
    const [shouldShowModal, setShouldShowModal] = useState<boolean>(false);
    const [cabs, setCabs] = useState<Cab[]>();
    const [booking, setBooking] = useState<Booking>(new Booking(0, "", "", 0, user.userId));

    useEffect(() => {
        repository.getAll()
            .then(data => {
                setCabs(data.map((cab) => new Cab(
                    cab.id,
                    cab.name,
                    cab.numberOfSeats,
                    cab.fuelType,
                    cab.registeredUntil,
                    cab.registrationPlates,
                    cab.driverId
                )));
            })
            .catch((error: Error) => {
                console.log(error);
            });
    }, []);

    const handleCabCardOnClick = (driverId: number): void => {
        setBooking(booking.setDriverId(driverId))
        setShouldShowModal(true);
    };

    const handleModalClose = () => {
        setBooking(booking.setDriverId(0))
        setShouldShowModal(false);
    }

    return (
        <div>
            <Hero image={image} heading="Rent a cab" />
            <main className="container py-5">
                <ContentCard heading="Available cabs">
                    <>
                        <div className="row">
                            <div className="col-12">
                                <div className="row mb-3">
                                    <div className="col-8 col-sm-9 col-md-10">
                                        <input className="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search" />
                                    </div>
                                    <div className="col-4 col-sm-3 col-md-2">
                                        <button className="btn btn-outline-warning my-sm-0 w-100" type="submit">Search</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="row">
                            {cabs?.map(cab => (
                                <div key={cab.id} className="col-12 col-md-4 col-lg-3">
                                    <CabCard
                                        name={cab.name}
                                        numberOfSeats={cab.numberOfSeats}
                                        driverId={cab.driverId}
                                        onClick={handleCabCardOnClick}
                                    />
                                </div>
                            ))}
                        </div>
                    </>
                </ContentCard>
            </main>
            <BookACabModal
                shouldShowModal={shouldShowModal}
                handleModalClose={handleModalClose}
                booking={booking}
                setBooking={setBooking}
                bookingRepository={bookingRepository}
            />
            <Footer />
        </div>
    );
}
