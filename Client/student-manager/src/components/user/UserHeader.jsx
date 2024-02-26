import { useContext, useEffect, useState } from "react"
import { UserContext } from "../../contexts/UserContext"
import { Image, NavDropdown, Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import ChangePassword from "./information/ChangePassword";
import { RoleContext } from "../../contexts/RoleContext";
import Button from 'react-bootstrap/Button';
import Offcanvas from 'react-bootstrap/Offcanvas';

const UserHeader = (props) => {

    const { role } = useContext(RoleContext)
    const { logout, user, randomParam } = useContext(UserContext)
    const [isEditInformationShow, setIsEditInformationShow] = useState(false);
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const handleEditInformationClose = () => setIsEditInformationShow(false);

    const handleEditInformationShow = () => setIsEditInformationShow(true);

    const handleLogout = () => {
        logout()
    }

    useEffect(() => {

    }, [randomParam]);

    return (<>
        {user.isAuth ?
            <div>
                <Nav>
                    {/* <NavDropdown
                        title={
                            <div className='name-user'>
                                <Image
                                    src={`${user.avatar}?${randomParam}`}
                                    roundedCircle={true}
                                    className="avatar-image-header"

                                />
                                &nbsp;{user.userName}
                            </div>
                        }
                    >
                        <NavDropdown.Item><Nav.Link as={Link} to="/user-information"><i class="fa-solid fa-address-card"></i>&nbsp; Profile</Nav.Link></NavDropdown.Item>
                        {user.role == role.Admin.role && (
                            <NavDropdown.Item><Nav.Link as={Link} to="/admin"><i class="fa-solid fa-user-tie"></i>&nbsp; {role.Admin.name}</Nav.Link></NavDropdown.Item>
                        )}
                        {user.role == role.Manager.role && (
                            <NavDropdown.Item><Nav.Link as={Link} to="/manager"><i class="fa-solid fa-bars-progress"></i>&nbsp; {role.Manager.name}</Nav.Link></NavDropdown.Item>
                        )}
                        <NavDropdown.Item><Nav.Link as={Link} onClick={handleEditInformationShow}><i class="fa-solid fa-key"></i>&nbsp; Change Password</Nav.Link></NavDropdown.Item>
                        <NavDropdown.Divider />
                        <div style={{
                            background: 'yellow',
                            color: 'white'
                        }}>
                            <NavDropdown.Item><Nav.Link as={Link} onClick={handleLogout}><i class="fa-solid fa-right-from-bracket"></i>&nbsp; Log Out</Nav.Link></NavDropdown.Item>
                        </div>
                    </NavDropdown> */}
                    <div className='name-user' onClick={handleShow}>
                        <Image
                            src={`${user.avatar}?${randomParam}`}
                            roundedCircle={true}
                            className="avatar-image-header"
                        />
                        &nbsp;{user.userName}
                    </div>
                    <Offcanvas show={show} onHide={handleClose}>
                        <Offcanvas.Header>
                            <Offcanvas.Title>
                                <Image
                                    src={`${user.avatar}?${randomParam}`}
                                    roundedCircle={true}
                                    className="avatar-image-header"
                                />
                                &nbsp;{user.userName}
                            </Offcanvas.Title>
                        </Offcanvas.Header>
                        <Offcanvas.Body>
                            <div>
                                <Nav.Link as={Link} to="/user-information"><i class="fa-solid fa-address-card"></i>&nbsp; Profile</Nav.Link>
                            </div>
                            <div>
                                {user.role == role.Admin.role && (
                                    <Nav.Link as={Link} to="/admin"><i class="fa-solid fa-user-tie"></i>&nbsp; {role.Admin.name}</Nav.Link>
                                )}
                                {user.role == role.Manager.role && (
                                    <Nav.Link as={Link} to="/manager"><i class="fa-solid fa-bars-progress"></i>&nbsp; {role.Manager.name}</Nav.Link>
                                )}
                            </div>
                            <div>
                                <Nav.Link as={Link} onClick={handleEditInformationShow}><i class="fa-solid fa-key"></i>&nbsp; Change Password</Nav.Link>
                            </div>
                            <div>
                                <NavDropdown.Divider />
                            </div>
                            <div style={{
                                color: 'red'
                            }}>
                                <Nav.Link as={Link} onClick={handleLogout}><i class="fa-solid fa-right-from-bracket"></i>&nbsp; Log Out</Nav.Link>
                            </div>
                        </Offcanvas.Body>
                    </Offcanvas>
                </Nav>
                <ChangePassword show={isEditInformationShow} handleClose={handleEditInformationClose} />
            </div>
            :
            <div
                roundedCircle={true}
                className='login-btn'
            >
                <Nav.Link as={Link} to="/auth">
                    <div>
                        <i class="fa-solid fa-arrow-right-to-bracket"></i>
                    </div>
                </Nav.Link>
            </div>


        }
    </>)
}

export default UserHeader