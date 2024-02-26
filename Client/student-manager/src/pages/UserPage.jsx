import React, { useContext, useEffect, useState } from 'react';
import Header from '../components/Header';
import Footer from '../components/Footer'
import { GetAllRole } from '../api/services/AuthService';
import { RoleContext } from '../contexts/RoleContext';
import { toast } from 'react-toastify';
import UserRoute from '../routes/user/UserRoute';
import '../assets/styles/App.scss';

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
            <Header />
            <UserRoute />
            <Footer />
        </div>
    </>);
}

export default UserPage;
