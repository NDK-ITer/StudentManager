import React, { useContext, useEffect, useState } from 'react';
import { UserContext } from '../../contexts/UserContext';
import { useNavigate } from 'react-router-dom';
import { toast } from "react-toastify";
import { GetMyInformation } from '../../api/services/AuthService';
import '../../assets/styles/UserInformation.scss'
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import Profile from './information/Profile';
import { Image, Card } from 'react-bootstrap';
import 'react-image-crop/dist/ReactCrop.css';
import EditAvatar from './information/EditAvatar';

const UserInformation = () => {

    let navigate = useNavigate()
    const { user ,randomParam, isAuth} = useContext(UserContext)
    const [userInformation, setUserInformation] = useState({})
    const [isEditAvatarShow, setIsEditAvatarShow] = useState(false);

    const handleEditAvatarClose = () => setIsEditAvatarShow(false);

    const handleEditAvatarShow = () => setIsEditAvatarShow(true);

    const getInformation = async () => {
        try {
            const res = await GetMyInformation()
            if (res.State === 1) {
                setUserInformation(res.Data)
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    useEffect(() => {
        if (!user.isAuth) {
            navigate('/auth')
            return
        }
        getInformation()
        return
    }, []);
    return (<>
        {userInformation && (
            <div>
                <div className='header-information' style={{ backgroundImage: `url(${user.avatar}?${randomParam})` }}>
                    <div className='line-avatar' >
                        <Card style={{ width: '18rem', borderRadius: '100px' }}>
                            <Card.Body>
                                <div className="avatar-wrapper">
                                    <Image
                                        src={`${user.avatar}?${randomParam}`}
                                        roundedCircle
                                        className="avatar-image"
                                        onClick={handleEditAvatarShow}
                                    />
                                    <div
                                        className="edit-overlay"
                                        onClick={handleEditAvatarShow}
                                    >
                                        <i class="fa-solid fa-pen"></i>
                                    </div>
                                    <EditAvatar show={isEditAvatarShow} handleClose={handleEditAvatarClose} />
                                </div>
                                <Card.Title>{user.userName}</Card.Title>
                            </Card.Body>
                        </Card>
                    </div>
                </div>
                <div className='body-information'>
                    <Tabs
                        defaultActiveKey="profile"
                        id="uncontrolled-tab-example"
                        className="mb-3"
                        fill
                    >
                        <Tab eventKey="profile"
                            title="Profile"
                        >
                            <Profile data={user} />
                        </Tab>
                        
                        <Tab eventKey="post"
                            title="Post"
                        >
                            <h1>POST</h1>
                        </Tab>
                    </Tabs>
                </div>
            </div>
        )}
    </>)
}
export default UserInformation