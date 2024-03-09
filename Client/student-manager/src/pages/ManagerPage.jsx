import '../assets/styles/manager/Manager.scss'
import React, { useContext, useEffect, useState } from 'react';
import Sidebar from '../components/manager/Sidebar'
import ManagerRoute from '../routes/manager/ManagerRoute'
import ManagerBackground from '../assets/images/manager-background.png'
import { UserContext } from '../contexts/UserContext';
import { GetFacultyById, SetStateFaculty } from '../api/services/FacultyService';
import { toast } from 'react-toastify';

const ManagerPage = () => {

    const { user } = useContext(UserContext)
    const [faculty, setFaculty] = useState()

    const updateFaculty = (newData) => {
        setFaculty(prevState => ({
            ...prevState,
            ...newData
        }));
    };


    const getFaculty = async () => {
        try {
            const res = await GetFacultyById(user.facultyId)
            if (res.State === 1) {
                setFaculty(res.Data)
            } else {
                toast.warning(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const OpenOrCloseFaculty = async () => {
        try {
            const res = await SetStateFaculty(user.facultyId)
            if (res.State === 1) {
                updateFaculty({isOpen: res.Data.isOpen})
            } else {
                toast.warning(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const handleSetStateClick = () => {
        OpenOrCloseFaculty();
    };

    useEffect(() => {
        getFaculty()
    }, [])
    return (<>
        <div className='manager-main manager-image-container' style={{ backgroundImage: `url(${ManagerBackground})` }}>
            {faculty && (<Sidebar faculty={faculty} setState={handleSetStateClick}/>)}
            <ManagerRoute />
        </div>
    </>);
}

export default ManagerPage;
