import Table from "./Table";
import TableProps from "./TableProps";

export default function TableContent(props: TableProps) {
    const { headers, rows } = props;

    return (
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
                <Table headers={headers} rows={rows} />
            </div>
            <div className="col-12">
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
            </div>
        </div>
    );
}