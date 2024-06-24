import { useEffect, useState } from "react";

import Hero from "../../common/hero/Hero";
import image from "../../../assets/images/rent-cab-hero.jpg";
import Footer from "../../common/footer/Footer";
import ContentCard from "../common/content-card/ContentCard";
import Cab from "../../../modules/cab/Cab";
import CabCard from "../../../modules/cab/cab-card/CabCard";
import PageProps from "../../common/props/PageProps";
import ICabRepository from "../../../modules/cab/cab-repository/ICabRepository";

export default function RentCab(props: PageProps<ICabRepository>) {
    const { repository } = props;
    const [cabs, setCabs] = useState<Cab[]>();

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
                                <div className="col-12 col-md-4 col-lg-3">
                                    <CabCard name={cab.name} numberOfSeats={cab.numberOfSeats} />
                                </div>
                            ))}
                        </div>
                    </>
                </ContentCard>
            </main>
            <Footer />
        </div>
    );
}
