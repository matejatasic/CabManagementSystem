import { Nav, NavDropdown } from "react-bootstrap";
import NavbarUserInformationProps from "./NavbarUserInformationProps";

export default function NavbarUserInformation(props: NavbarUserInformationProps) {
    const { username } = props;

    return (
        <Nav>
            <NavDropdown title={username} id="basic-nav-dropdown">
                <NavDropdown.Item href="#">Profile</NavDropdown.Item>
                <NavDropdown.Item href="#">Settings</NavDropdown.Item>
                <NavDropdown.Divider />
                <NavDropdown.Item href="#">Logout</NavDropdown.Item>
            </NavDropdown>
        </Nav>
    );
}