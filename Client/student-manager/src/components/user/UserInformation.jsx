import React, { useContext, useEffect, useState } from 'react';
import { Card, Image } from 'react-bootstrap';
import { UserContext } from '../../contexts/UserContext';
import { Link, useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from "react-toastify";
import { GetMyInformation } from '../../api/services/AuthService';
import '../../assets/styles/UserInformation.scss'
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import Profile from './information/Profile';

const UserInformation = () => {

    const { user } = useContext(UserContext)
    let navigate = useNavigate()
    const [userInformation, setUserInformation] = useState({})

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
                    <Card style={{ width: '18rem' }}>
                        <Card.Body>
                            <div className="avatar-wrapper ">
                                <Image src={user.avatar} roundedCircle style={{ width: '100px', height: '100px' }} className="avatar-image" />
                                <div className="edit-overlay"><i class="fa-solid fa-pen-to-square"></i></div>
                            </div>
                            <Card.Title>{userInformation.fullName}</Card.Title>
                        </Card.Body>
                    </Card>
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