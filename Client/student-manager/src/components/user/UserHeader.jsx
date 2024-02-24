import { useContext } from "react"
import { UserContext } from "../../contexts/UserContext"
import { Image, NavDropdown, Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';

const UserHeader = (props) => {

    const { logout, user } = useContext(UserContext)

    const handleLogout = () => {
        logout()
    }

    return (<>
        {user.isAuth ?
            <Nav>
                <NavDropdown
                    title={
                        <div className='name-user'>
                            <Image
                                src={user.avatar}
                                roundedCircle={true}
                                style={{
                                    borderRadius: "5%",
                                    height: "50px",
                                    width: "50px"
                                }}

                            />
                            &nbsp;{user.name}
                        </div>
                    }
                >
                    <NavDropdown.Item><Nav.Link as={Link} to="/main-information"><i class="fa-solid fa-address-card"></i>&nbsp;Profile</Nav.Link></NavDropdown.Item>
                    <NavDropdown.Divider />
                    <div style={{
                        background: 'yellow',
                        color: 'white'
                    }}>
                        <NavDropdown.Item><Nav.Link as={Link} onClick={handleLogout}><i class="fa-solid fa-right-from-bracket"></i>&nbsp;Log Out</Nav.Link></NavDropdown.Item>
                    </div>
                </NavDropdown>
            </Nav>

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