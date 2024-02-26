import React, { useState } from 'react';
import '../assets/styles/admin/Admin.scss'
import MainContent from '../components/admin/MainContent';
import Sidebar from '../components/admin/Sidebar';

const AdminPage = () => {

    const [showSidebar, setShowSidebar] = useState(false);

    const toggleSidebar = () => {
        setShowSidebar(!showSidebar);
    };

    return (<>
        <div>
            <Sidebar isShow = {showSidebar}/>
            <MainContent HideSidebar = {toggleSidebar}/>
        </div>
    </>);
}

export default AdminPage;
