import { ArrowUp, PencilFill, TrashFill } from "react-bootstrap-icons";
import ContentCard from "../common/content-card/ContentCard";

export default function Users() {
    return (
        <ContentCard>
            <>
                <div className="row">
                    <div className="col-12">
                        <div className="row">
                            <div className="col-12 col-sm-9 col-lg-10 mb-3">
                                <div className="row">
                                    <div className="col-8 col-lg-10">
                                        <input type="text" className="form-control" />
                                    </div>
                                    <div className="col-4 col-lg-2">
                                        <button className="btn btn-outline-dark">Search</button>
                                    </div>
                                </div>
                            </div>
                            <div className="col-12 col-sm-3 col-lg-2">
                                <div className="row">
                                    <div className="col-7 col-sm-0"></div>
                                    <div className="col-5 col-sm-12">
                                        <button className="btn btn-primary w-100">Add New</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
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
                                    <td>
                                        <button className="btn btn-success me-3"><PencilFill /></button>
                                        <button className="btn btn-danger"><TrashFill /></button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>2023-04-03</td>
                                    <td>107th St, Chicago</td>
                                    <td>99th St, Chicago</td>
                                    <td><span className="badge bg-primary">Pending</span></td>
                                    <td>
                                        <button className="btn btn-success me-3"><PencilFill /></button>
                                        <button className="btn btn-danger"><TrashFill /></button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>2023-04-03</td>
                                    <td>107th St, Chicago</td>
                                    <td>99th St, Chicago</td>
                                    <td><span className="badge bg-primary">Pending</span></td>
                                    <td>
                                        <button className="btn btn-success me-3"><PencilFill /></button>
                                        <button className="btn btn-danger"><TrashFill /></button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>2023-04-03</td>
                                    <td>107th St, Chicago</td>
                                    <td>99th St, Chicago</td>
                                    <td><span className="badge bg-primary">Pending</span></td>
                                    <td>
                                        <button className="btn btn-success me-3"><PencilFill /></button>
                                        <button className="btn btn-danger"><TrashFill /></button>
                                    </td>
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
    );
}