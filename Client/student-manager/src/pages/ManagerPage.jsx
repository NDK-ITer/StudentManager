import '../assets/styles/manager/Manager.scss'
import React, { useContext, useEffect, useState } from 'react';
import Sidebar from '../components/manager/Sidebar'
import ManagerRoute from '../routes/manager/ManagerRoute'
import ManagerBackground from '../assets/images/manager-background.png'
import { UserContext } from '../contexts/UserContext';
import { GetFacultyById, SetStateFaculty } from '../api/services/FacultyService';
import { toast } from 'react-toastify';
import { Button, Modal } from "react-bootstrap";
import { Form } from 'react-bootstrap';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';

const ManagerPage = () => {

    const { user } = useContext(UserContext)
    const [faculty, setFaculty] = useState()
    const [deadlineValue, setDeadlineValue] = useState()

    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

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
            const deadline = deadlineValue.toISOString()
            const res = await SetStateFaculty(user.facultyId, deadline)
            if (res.State === 1) {
                updateFaculty({ isOpen: res.Data.isOpen })
            } else {
                toast.warning(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const handleChange = (date) => {
        setDeadlineValue(date);
    };

    const handleSetStateClick = () => {
        handleShow()
    };

    useEffect(() => {
        getFaculty()
    }, [])
    return (<>
        <div className='manager-main manager-image-container' style={{ backgroundImage: `url(${ManagerBackground})` }}>
            {faculty && (<Sidebar faculty={faculty} setState={handleSetStateClick} />)}
            <Modal show={show} onHide={handleClose} fullscreen={false}>
                <Modal.Header closeButton>
                    <Modal.Title>Set deadline</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group>
                            <Form.Label>Deadline Date and Time</Form.Label>
                            <DatePicker
                                selected={deadlineValue}
                                onChange={handleChange}
                                showTimeSelect
                                timeFormat="HH:mm"
                                timeIntervals={15}
                                timeCaption="Time"
                                dateFormat="MMMM d, yyyy h:mm aa"
                            />
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={() => {
                        handleClose();
                        OpenOrCloseFaculty();
                    }}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
            <ManagerRoute />
        </div>
    </>);
}

export default ManagerPage;
