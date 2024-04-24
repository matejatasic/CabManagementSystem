import { CarFrontFill, Search } from "react-bootstrap-icons";

import Hero from "../../common/hero/Hero";
import image from "../../assets/images/rent-cab-hero.jpg";
import Footer from "../../common/footer/Footer";
import ContentCard from "../../common/content-card/ContentCard";

export default function RentCab() {
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
                            <div className="col-12 col-md-4 col-lg-3">
                                <a href="#" className="text-decoration-none">
                                    <div className="card mb-3">
                                        <div className="row g-0">
                                            <div className="col-12">
                                                <div className="card-body">
                                                    <CarFrontFill className="d-inline-block me-2 " />
                                                    <h5 className="card-title d-inline-block">Skoda Fabia</h5>
                                                    <p className="card-text">5 Seater</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div className="col-12 col-md-4 col-lg-3">
                                <a href="#" className="text-decoration-none">
                                    <div className="card mb-3">
                                        <div className="row g-0">
                                            <div className="col-12">
                                                <div className="card-body">
                                                    <CarFrontFill className="d-inline-block me-2 " />
                                                    <h5 className="card-title d-inline-block">Dacia Logan</h5>
                                                    <p className="card-text">5 Seater</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div className="col-12 col-md-4 col-lg-3">
                                <a href="#" className="text-decoration-none">
                                    <div className="card mb-3">
                                        <div className="row g-0">
                                            <div className="col-12">
                                                <div className="card-body">
                                                    <CarFrontFill className="d-inline-block me-2 " />
                                                    <h5 className="card-title d-inline-block">Hyundai i10</h5>
                                                    <p className="card-text">6 Seater</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div className="col-12 col-md-4 col-lg-3">
                                <a href="#" className="text-decoration-none">
                                    <div className="card mb-3">
                                        <div className="row g-0">
                                            <div className="col-12">
                                                <div className="card-body">
                                                    <CarFrontFill className="d-inline-block me-2 " />
                                                    <h5 className="card-title d-inline-block">Citroen C1</h5>
                                                    <p className="card-text">6 Seater</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </>
                </ContentCard>
            </main>
            <Footer />
        </div>
    );
}