import { useContext, useEffect, useState } from "react"
import { UserContext } from "../../contexts/UserContext"
import { Image, NavDropdown, Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import ChangePassword from "./information/ChangePassword";

const UserHeader = (props) => {

    const { logout, user, randomParam } = useContext(UserContext)
    const [isEditInformationShow, setIsEditInformationShow] = useState(false);

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
                    <NavDropdown
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
                        <NavDropdown.Item><Nav.Link as={Link} to="/user-information"><i class="fa-solid fa-address-card"></i>&nbsp;Profile</Nav.Link></NavDropdown.Item>
                        <NavDropdown.Item><Nav.Link as={Link} onClick={handleEditInformationShow}><i class="fa-solid fa-key"></i>&nbsp;Change Password</Nav.Link></NavDropdown.Item>
                        <NavDropdown.Divider />
                        <div style={{
                            background: 'yellow',
                            color: 'white'
                        }}>
                            <NavDropdown.Item><Nav.Link as={Link} onClick={handleLogout}><i class="fa-solid fa-right-from-bracket"></i>&nbsp;Log Out</Nav.Link></NavDropdown.Item>
                        </div>
                    </NavDropdown>
                </Nav>
                <ChangePassword show={isEditInformationShow} handleClose={handleEditInformationClose}/>
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