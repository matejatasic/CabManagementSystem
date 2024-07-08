import { Nav, NavDropdown } from "react-bootstrap";
import { useDispatch } from "react-redux";

import NavbarUserInformationProps from "./NavbarUserInformationProps";
import { logout } from "../store/slices/user.slice";
import { useNavigate } from "react-router-dom";

export default function NavbarUserInformation(props: NavbarUserInformationProps) {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { username } = props;

    const handleLogout = () => {
        dispatch(logout());
        navigate("/");
    };

    return (
        <Nav>
            <NavDropdown title={username} id="basic-nav-dropdown">
                <NavDropdown.Item href="#">Profile</NavDropdown.Item>
                <NavDropdown.Item href="#">Settings</NavDropdown.Item>
                <NavDropdown.Divider />
                <NavDropdown.Item onClick={() => handleLogout()}>Logout</NavDropdown.Item>
            </NavDropdown>
        </Nav>
    );
}