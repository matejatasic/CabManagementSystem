import "./Sidebar.scss"

export default function Sidebar() {
    const sidebarNavItems = [
        {
            label: "Statistics",
            to: ""
        },
    ];

    return (
        <div id="sidebar" className="col-12 col-md-3 col-xl-2 px-2 py-3 px-0 bg-dark">
            <div className="d-flex flex-md-column align-items-center px-3 pt-2 text-white">
                <p className="d-flex flex-row align-items-md-center pb-md-3 me-3 me-md-0 mb-0 mb-md-0 text-white text-decoration-none">
                    <span className="fs-5 d-inline">Menu</span>
                </p>
                <ul className="nav nav-pills flex-row flex-md-column mb-0 align-items-center align-items-start" id="menu">
                    {sidebarNavItems.map(item => {
                        return (
                            <li className="nav-item me-2 me-md-0">
                                <a href={`/admin/${item.to}`} className="nav-link align-middle px-0">
                                    <span className="ms-1 d-inline">{item.label}</span>
                                </a>
                            </li>
                        );
                    })}
                </ul>
            </div>
        </div>
    );
}