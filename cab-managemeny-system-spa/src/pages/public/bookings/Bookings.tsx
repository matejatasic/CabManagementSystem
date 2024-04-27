import { ArrowUp } from "react-bootstrap-icons";

import "./Bookings.scss"
import ContentCard from "../../../common/content-card/ContentCard";

export default function Bookings() {
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
                                            <th>Date Booked <span className="sort-arrow"><ArrowUp /></span></th>
                                            <th>From Address <span className="sort-arrow"><ArrowUp /></span></th>
                                            <th>To Address <span className="sort-arrow"><ArrowUp /></span></th>
                                            <th>Status <span className="sort-arrow"><ArrowUp /></span></th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>2023-04-03</td>
                                            <td>107th St, Chicago</td>
                                            <td>99th St, Chicago</td>
                                            <td><span className="badge bg-primary">Pending</span></td>
                                            <td><button className="btn btn-dark">View Details</button></td>
                                        </tr>
                                        <tr>
                                            <td>2023-04-03</td>
                                            <td>107th St, Chicago</td>
                                            <td>99th St, Chicago</td>
                                            <td><span className="badge bg-primary">Pending</span></td>
                                            <td><button className="btn btn-dark">View Details</button></td>
                                        </tr>
                                        <tr>
                                            <td>2023-04-03</td>
                                            <td>107th St, Chicago</td>
                                            <td>99th St, Chicago</td>
                                            <td><span className="badge bg-primary">Pending</span></td>
                                            <td><button className="btn btn-dark">View Details</button></td>
                                        </tr>
                                        <tr>
                                            <td>2023-04-03</td>
                                            <td>107th St, Chicago</td>
                                            <td>99th St, Chicago</td>
                                            <td><span className="badge bg-primary">Pending</span></td>
                                            <td><button className="btn btn-dark">View Details</button></td>
                                        </tr>
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