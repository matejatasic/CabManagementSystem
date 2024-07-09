import { Link } from "react-router-dom";
import CustomerRoutesEnum from "../../public/common/enums/CustomerRoutesEnum";

export default function NavbarAuthenticationButtons() {
    return (
        <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
                <Link to={CustomerRoutesEnum.Login} className="nav-link">Login</Link>
            </li>
            <li className="nav-item">
                <Link to={CustomerRoutesEnum.Register} className="nav-link">Register</Link>
            </li>
        </ul>
    );
}