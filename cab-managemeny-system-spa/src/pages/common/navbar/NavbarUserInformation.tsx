import { Nav, NavDropdown } from "react-bootstrap";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";

import NavbarUserInformationProps from "./NavbarUserInformationProps";
import { logout } from "../store/slices/user.slice";
import CustomerRoutesEnum from "../../public/common/enums/CustomerRoutesEnum";

export default function NavbarUserInformation(props: NavbarUserInformationProps) {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { username } = props;

    const handleLogout = () => {
        dispatch(logout());
        navigate(CustomerRoutesEnum.Home);
    };

    const handleNavigateToBookings = () => {
        navigate(CustomerRoutesEnum.Bookings)
    };

    const handleNavigateToChangeProfileSettings = () => {
        navigate(CustomerRoutesEnum.ChangeProfileSettings)
    };

    return (
        <Nav>
            <NavDropdown title={username} id="basic-nav-dropdown">
                <NavDropdown.Item onClick={() => handleNavigateToBookings()}>Bookings</NavDropdown.Item>
                <NavDropdown.Item onClick={() => handleNavigateToChangeProfileSettings()}>Change profile settings</NavDropdown.Item>
                <NavDropdown.Divider />
                <NavDropdown.Item onClick={() => handleLogout()}>Logout</NavDropdown.Item>
            </NavDropdown>
        </Nav>
    );
}