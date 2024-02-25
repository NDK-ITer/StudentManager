import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import '../assets/styles/Header.scss'
import UserHeader from './user/UserHeader';
import { useLocation, Link } from 'react-router-dom';
import Image from 'react-bootstrap/Image';
import MainLogo from '../assets/images/main-logo.png'

const Header = () => {

    const location = useLocation()

    return (<>
        <div>
            <Navbar collapseOnSelect expand="lg" bg="dark" className="bg-body-tertiary">
                <Container>
                    <Navbar.Brand as={Link} to='/'>
                        <Image src={MainLogo} className='main-logo' />
                    </Navbar.Brand>
                    <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className="me-auto" activeKey={location.pathname}>
                            <Nav.Link href="#features">Features</Nav.Link>
                            <Nav.Link href="#pricing">Pricing</Nav.Link>
                            <NavDropdown title="Dropdown" id="collapsible-nav-dropdown">
                                <NavDropdown.Item as={Link} to="#action/3.1">one</NavDropdown.Item>
                                <NavDropdown.Item as={Link} to="#action/3.2">two</NavDropdown.Item>
                                <NavDropdown.Item as={Link} to="#action/3.3">three</NavDropdown.Item>
                                <NavDropdown.Divider />
                                <NavDropdown.Item as={Link} to="#action/3.4">end</NavDropdown.Item>
                            </NavDropdown>
                        </Nav>
                        <Nav>
                            <UserHeader />
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </div>
    </>)
}

export default Header