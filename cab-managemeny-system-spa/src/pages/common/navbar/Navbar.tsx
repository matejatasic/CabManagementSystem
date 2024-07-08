import { useSelector } from "react-redux";
import { Link } from "react-router-dom";

import RootState from "../store/state.type";
import NavbarAuthenticationButtons from "./NavbarAuthenticationButtons";
import NavbarUserInformation from "./NavbarUserInformation";

export default function Navbar() {
    const user = useSelector((state: RootState) => state.user)

    return (
        <nav className="navbar navbar-expand-lg bg-body-tertiary">
            <div className="container-fluid">
                <a href="#" className="navbar-brand">Navbar</a>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <Link to="/" className="nav-link">Home</Link>
                        </li>
                        <li className="nav-item">
                            <Link to="/rent-cab" className="nav-link">Rent a cab</Link>
                        </li>
                    </ul>
                    <div>
                        {
                            user.token !== "" ?
                            (
                                <NavbarUserInformation username={user.username} />
                            ) :
                            (
                                <NavbarAuthenticationButtons />
                            )
                        }
                    </div>
                </div>
            </div>
        </nav>
    );
}
