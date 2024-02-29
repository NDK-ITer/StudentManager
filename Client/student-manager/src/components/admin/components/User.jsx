import { useContext, useEffect, useState } from "react"
import {
    GetAllUser,
    SetLockUser
} from '../../../api/services/UserService'
import { toast } from "react-toastify"
import { Image, Card, Form, Col } from 'react-bootstrap';
import HomeAdmin from '../../../assets/images/home-admin.png'
import { RoleContext } from '../../../contexts/RoleContext'
import { Link } from "react-router-dom";


const User = () => {

    const { role } = useContext(RoleContext)
    const [listUser, setListUser] = useState([])
    const [searchTerm, setSearchTerm] = useState('');
    const [isOpenUserDetail, setIsUserDetail] = useState(Array(listUser.length).fill(false))

    const togglePopup = (index) => {
        const newIsOpenUserDetail = [...isOpenUserDetail];
        newIsOpenUserDetail[index] = !newIsOpenUserDetail[index];
        setIsUserDetail(newIsOpenUserDetail);
    };

    const handleSearchChange = (event) => {
        setSearchTerm(event.target.value);
    };

    const filteredListUser = listUser.filter(user => {
        const searchTermLower = searchTerm.toLowerCase();
        const userNameMatch = user.userName.toLowerCase().includes(searchTermLower);
        const emailMatch = user.email.toLowerCase().includes(searchTermLower);
        const roleMatch = user.authorize.role.toLowerCase().includes(searchTermLower);
        const roleNameMatch = user.authorize.name.toLowerCase().includes(searchTermLower);
        const fulNameMatch = user.fullName.toLowerCase().includes(searchTermLower);

        return userNameMatch || emailMatch || roleMatch || roleNameMatch || fulNameMatch;
    }
    );

    const getAllUser = async () => {
        try {
            const res = await GetAllUser()
            if (res.State === 1) {
                setListUser(res.Data.listUser)
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const updateListUser = (id, updatedProperty) => {
        setListUser(prevList => {
            return prevList.map(user => {
                if (user.id === id) {
                    return { ...user, ...updatedProperty };
                }
                return user;
            });
        });
    };

    const setLockUser = async (id) => {
        try {
            const res = await SetLockUser({ idUser: id })
            if (res.State === 1) {
                updateListUser(res.Data.id, { isLock: res.Data.isLock })
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    useEffect(() => {
        getAllUser()
    }, [])
    return (<>
        <div className="user-container-custom image-container"
            style={{
                backgroundImage: `url(${HomeAdmin})`
            }}
        >
            <div className="search-user">
                <div style={{
                    display: 'block',
                    margin: 'auto',
                    width: '50%'
                }}>
                    <Col xs="auto" >
                        <Form.Control
                            type="text"
                            placeholder="Search"
                            className=" mr-sm-2"
                            value={searchTerm}
                            onChange={handleSearchChange}
                            style={{
                                fontSize: '20px',
                                borderRadius: '100px !important'
                            }}
                        />
                    </Col>
                </div>
            </div>
            <div className="list-user">
                {
                    listUser && listUser.length > 0 && filteredListUser.map((item, index) => {
                        return (
                            <div key={item.id} className="user-element">
                                <Link to={{ pathname: `/admin/user/${item.id}` }} style={{ textDecoration: 'none' }}>
                                    <Card style={{
                                        width: '18rem',
                                        height: '100%',
                                        boxShadow: '10px 10px 10px rgba(2, 2, 2, 1.5)'
                                    }}>
                                        <Card.Header className={item.isVerify ? 'verify' : 'no-verify'}>
                                            <div className="text-warning">{item.isVerify ? '' : 'Not yet authenticated!'}</div>
                                            <Image src={item.avatar}
                                                roundedCircle
                                                className="avatar-user-item"
                                                onClick={() => togglePopup(index)}
                                            />
                                        </Card.Header>
                                        <Card.Body style={{
                                            background: 'white',
                                            Height: '100%'
                                        }}>
                                            <Card.Title>{item.userName}</Card.Title>
                                            <Card.Text>
                                                {(item.authorize.role == role.Admin.role) && (<div style={{ color: 'red' }}>{item.authorize.name}</div>)}
                                                {(item.authorize.role == role.Manager.role) && (<div style={{ color: 'blue' }}>{item.authorize.name}</div>)}
                                                {(item.authorize.role == role.User.role) && (<div style={{ color: 'green' }}>{item.authorize.name}</div>)}
                                            </Card.Text>
                                        </Card.Body>
                                        <Card.Footer className={item.isLock ? 'lock' : 'un-lock'}
                                            onClick={() => setLockUser(item.id)}
                                        >
                                            <div className="user-bool">
                                                <div >
                                                    {item.isLock ? <i class="fa-solid fa-lock"></i>
                                                        : <i class="fa-solid fa-lock-open"></i>}
                                                </div>
                                            </div>
                                        </Card.Footer>
                                    </Card>
                                </Link>
                            </div>
                        )
                    })
                }
            </div>
        </div>
    </>)
}

export default User