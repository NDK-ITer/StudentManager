import { Link } from "react-router-dom"
import { Image } from "react-bootstrap"
import MainLogo from "../../assets/images/main-logo.png"
import React from 'react';
import { Navbar, Nav } from 'react-bootstrap';

const Header = ({ handleShow }) => {
    return (<>
        <div className="header" style={{ background: 'linear-gradient(to left, blue, white) !important' }}>
            <Navbar style={{ background: 'linear-gradient(to left, blue, white) !important' }}>
                <Navbar.Brand href="#home">
                    <div className="logo-admin">
                        <Link to='/admin'>
                            <Image src={MainLogo} />
                        </Link>
                    </div>
                </Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav" className="justify-content-end">
                    <Nav className="mr-auto">
                        <div className="menu-header">
                            <Nav.Link onClick={handleShow}>
                                <i class="fa-solid fa-bars"></i>
                            </Nav.Link>
                        </div>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        </div>
    </>)
}
export default Header