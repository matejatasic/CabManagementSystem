import Sidebar from "../sidebar/Sidebar";
import DashboardProps from "./DashboardProps";

export default function Dashboard(props: DashboardProps) {
    const { heading, children } = props;

    return (
        <div className="container-fluid">
            <div className="row">
                <Sidebar />
                <div className="col-12 col-md-9 col-xl-10 py-5">
                    <div className="row">
                        <div className="col-12 text-center mb-5">
                            <h1>{ heading }</h1>
                            <hr />
                        </div>
                    </div>
                    { children }
                </div>
            </div>
        </div>
    );
}