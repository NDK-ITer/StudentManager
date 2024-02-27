import React, { useState } from 'react';
import '../assets/styles/admin/Admin.scss'
import AdminRoute from '../routes/admin/AdminRoute'
import Header from '../components/admin/Header';
import Sidebar from '../components/admin/Sidebar'


const AdminPage = () => {
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    return (<>
        <Sidebar show={show} handleClose={handleClose} />
        <div>
            <Header handleShow={handleShow} />
            <AdminRoute />
        </div>
    </>);
}

export default AdminPage;
