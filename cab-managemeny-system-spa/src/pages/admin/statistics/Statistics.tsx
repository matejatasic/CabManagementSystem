import "./Statistics.scss"
import Sidebar from "../Sidebar/Sidebar";

export default function Statistics() {
    const statisticsItems = [
        {
            label: "Accounts",
            count: 0
        },
        {
            label: "Employees",
            count: 0
        },
        {
            label: "Cars",
            count: 0
        },
        {
            label: "Routes taken",
            count: 0
        },
    ];

    return (
        <div className="container-fluid">
            <div className="row">
                <Sidebar />
                <div className="col-12 col-md-9 col-xl-10 py-5">
                    <div className="row">
                        <div className="col-12 text-center mb-5">
                            <h1>Statistics</h1>
                            <hr />
                        </div>
                    </div>
                    <div className="row">
                        {statisticsItems.map(item => {
                            return (
                                <div className="col-12 col-md-6 mb-3">
                                    <div className="card w-100">
                                        <div className="card-body text-center">
                                            <h2 className="card-title">{item.label}</h2>
                                            <p className="card-text statistic-count">{item.count}</p>
                                        </div>
                                    </div>
                                </div>
                            );
                        })}
                    </div>
                </div>
            </div>
        </div>
    );
}