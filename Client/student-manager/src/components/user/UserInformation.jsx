import React, { useContext, useEffect, useState } from 'react';
import { UserContext } from '../../contexts/UserContext';
import { Link, useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from "react-toastify";
import { GetMyInformation } from '../../api/services/AuthService';
import '../../assets/styles/UserInformation.scss'
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import Profile from './information/Profile';
import { Image, Card } from 'react-bootstrap';
import 'react-image-crop/dist/ReactCrop.css';
import CutImage from './information/CutImage';

const UserInformation = () => {

    const { user } = useContext(UserContext)
    let navigate = useNavigate()
    const [userInformation, setUserInformation] = useState({})
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

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
    });
    return (<>
        {userInformation && (
            <div>
                <div className='header'>
                    <div className='line-avatar' >
                        <Card style={{ width: '18rem', borderRadius: '100px' }}>
                            <Card.Body>
                                <div className="avatar-wrapper ">
                                    <Image 
                                        src={user.avatar} 
                                        roundedCircle style={{ width: '100px', height: '100px' }} 
                                        className="avatar-image" 
                                        onClick={handleShow}
                                    />
                                    <div
                                        className="edit-overlay"
                                        onClick={handleShow}
                                    >
                                        <i class="fa-solid fa-pen"></i>
                                    </div>
                                    <CutImage show ={show} handleClose = {handleClose}/>
                                </div>
                                <Card.Title>{userInformation.userName}</Card.Title>
                            </Card.Body>
                        </Card>
                    </div>
                </div>
                <div className='body'>
                    <Tabs
                        defaultActiveKey="profile"
                        id="uncontrolled-tab-example"
                        className="mb-3"
                        fill
                    >
                        <Tab eventKey="profile" title="Profile">
                            <Profile data={userInformation} />
                        </Tab>
                        <Tab eventKey="post" title="Post">
                            <h1>POST</h1>
                        </Tab>
                    </Tabs>
                </div>
            </div>
        )}
        <ToastContainer />
    </>)
}
export default UserInformation