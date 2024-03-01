import React, { useContext, useEffect } from 'react';
import Header from '../components/Header';
import Footer from '../components/Footer'
import { GetAllRole } from '../api/services/AuthService';
import { RoleContext } from '../contexts/RoleContext';
import { toast } from 'react-toastify';
import UserRoute from '../routes/user/UserRoute';
import '../assets/styles/user/User.scss'
import UserBackground from '../assets/images/user-background.png'

const UserPage = () => {

    const { UpdateListRole } = useContext(RoleContext)
    const getRole = async () => {
        try {
            const res = await GetAllRole()
            if (res.State === 1) {
                UpdateListRole(res.Data.listRole)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    useEffect(() => {
        getRole()
    }, [])

    return (<>
        <div className=''>
            <div className='main-content user-image-container' style={{ backgroundImage: `url(${UserBackground})` }}>
                <Header />
                <UserRoute />
            </div>
        </div>
    </>);
}

export default UserPage;
