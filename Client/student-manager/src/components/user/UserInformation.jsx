import React, { useContext, useEffect, useState } from 'react';
import { UserContext } from '../../contexts/UserContext';
import { useNavigate } from 'react-router-dom';
import { toast } from "react-toastify";
import { GetMyInformation } from '../../api/services/AuthService';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import Profile from './information/Profile';
import Post from './information/Post';
import { Image, Card } from 'react-bootstrap';
import 'react-image-crop/dist/ReactCrop.css';
import EditAvatar from './information/EditAvatar';

const UserInformation = () => {

    let navigate = useNavigate()
    const { user, randomParam } = useContext(UserContext)
    const [userInformation, setUserInformation] = useState({})
    const [isEditAvatarShow, setIsEditAvatarShow] = useState(false);
    const [listPost, setListPost] = useState([])

    const handleEditAvatarClose = () => setIsEditAvatarShow(false);

    const handleEditAvatarShow = () => setIsEditAvatarShow(true);

    const getInformation = async () => {
        try {
            const res = await GetMyInformation()
            if (res.State === 1) {
                setUserInformation(res.Data)
                setListPost(res.Data.listPost)
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
            <div className='profile-content' >
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
                        className="mb-5"
                        fill
                    >
                        <Tab eventKey="profile" title={<span className="tab-title">Profile</span>}>
                            <Profile data={user} />
                        </Tab>
                        <Tab eventKey="post" title={<span className="tab-title">Post</span>}>
                            <Post data={listPost}/>
                        </Tab>
                    </Tabs>
                </div>
            </div>
        )}
    </>)
}
export default UserInformation