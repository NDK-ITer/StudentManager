import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import '../assets/styles/Header.scss'
import UserHeader from './user/UserHeader';
import { useLocation, Link } from 'react-router-dom';

const Header = () => {

    const location = useLocation()

    return (<>
        <Navbar collapseOnSelect expand="lg" className="bg-body-tertiary">
            <Container>
                <Navbar.Brand as={Link} to='/'>React-Bootstrap</Navbar.Brand>
                <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                <Navbar.Collapse id="responsive-navbar-nav">
                    <Nav className="me-auto" activeKey={location.pathname}>
                        <Nav.Link href="#features">Features</Nav.Link>
                        <Nav.Link href="#pricing">Pricing</Nav.Link>
                        <NavDropdown title="Dropdown" id="collapsible-nav-dropdown">
                            <NavDropdown.Item as={Link} href="#action/3.1">one</NavDropdown.Item>
                            <NavDropdown.Item as={Link} href="#action/3.2">two</NavDropdown.Item>
                            <NavDropdown.Item as={Link} href="#action/3.3">three</NavDropdown.Item>
                            <NavDropdown.Divider />
                            <NavDropdown.Item as={Link} href="#action/3.4">end</NavDropdown.Item>
                        </NavDropdown>
                    </Nav>
                    <Nav>
                        <UserHeader />
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    </>)
}

export default Header