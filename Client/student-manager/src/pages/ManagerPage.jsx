import '../assets/styles/manager/Manager.scss'
import React from 'react';
import Sidebar from '../components/manager/Sidebar'
import ManagerRoute from '../routes/manager/ManagerRoute'
import ManagerBackground from '../assets/images/manager-background.png'

const ManagerPage = () => {
    return (<>
        <div className='manager-main manager-image-container' style={{backgroundImage: `url(${ManagerBackground})`}}>
            <Sidebar/>
            <ManagerRoute/>
        </div>
    </>);
}

export default ManagerPage;
