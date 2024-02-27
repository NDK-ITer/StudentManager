import { useEffect, useState } from "react"
import {
    GetAllUser,
    SetLockUser
} from '../../../api/services/UserService'
import { toast } from "react-toastify"
import { Image, Card } from 'react-bootstrap';
import HomeAdmin from '../../../assets/images/home-admin.png'
const User = () => {

    const [listUser, setListUser] = useState([])

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
            </div>
            <div className="list-user">
                {
                    listUser && listUser.length > 0 && listUser.map((item, index) => {
                        return (
                            <div className="user-element">
                                <Card style={{
                                    width: '18rem',
                                    height: '50%'
                                }}>
                                    <Card.Header className={item.isVerify ? 'verify' : 'no-verify'}>
                                        <div className="text-warning">{item.isVerify ? '' : 'Not yet authenticated!'}</div>
                                        <Image src={item.avatar}
                                            roundedCircle
                                            className="avatar-user-item"
                                        />
                                    </Card.Header>
                                    <Card.Body style={{
                                        background: 'white',
                                        minHeight: '100%'
                                    }}>
                                        <Card.Title>{item.userName}</Card.Title>
                                        <Card.Text>

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
                            </div>
                        )
                    })
                }
            </div>
        </div>
    </>)
}

export default User