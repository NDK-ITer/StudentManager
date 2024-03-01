import { useContext, useEffect, useState } from 'react';
import { Modal, Image, Tab, Tabs, Button, Form } from 'react-bootstrap';
import { RoleContext } from '../../../contexts/RoleContext';
import { toast } from 'react-toastify';
import { GetUserById, SetUser, SetManager } from '../../../api/services/UserService'
import { GetAllFaculty } from '../../../api/services/FacultyService'
import { useParams } from 'react-router-dom';

const UserDetail = () => {

    const { role } = useContext(RoleContext)
    const [user, setUser] = useState()
    const { userId } = useParams();
    const [showVerify, setShowVerify] = useState(false);
    const [listFacultyOption, setListFacultyOption] = useState()
    const [selectedFaculty, setSelectedFaculty] = useState('');

    const handleSelectChange = (event) => {
        setSelectedFaculty(event.target.value);
    };

    const getAllFaculty = async () => {
        try {
            const res = await GetAllFaculty()
            if (res.State === 1) {
                setListFacultyOption(res.Data.listFaculty)
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const handleClose = () => setShowVerify(false);

    const handleShow = () => setShowVerify(true);

    const updateUserFields = (newFields) => {
        setUser(prevUser => ({
            ...prevUser,
            ...newFields
        }));
    };


    const getUserById = async (id) => {
        try {
            const res = await GetUserById({ id: id })
            if (res.State === 1) {
                updateUserFields(res.Data)
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const changeManager = async (idUser) => {
        try {
            const res = await SetManager(idUser, selectedFaculty)
            if (res.State === 1) {
                updateUserFields(res.Data)
                console.log(user)
                toast.success("update successful")
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const changeUser = async (id) => {
        try {
            const res = await SetUser(id)
            if (res.State === 1) {
                updateUserFields(res.Data)
                toast.success("update successful")
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    useEffect(() => {
        getUserById(userId)
        getAllFaculty()
    }, [])

    return (<>
        {user && (<>
            <div className={user.isVerify ? 'verify' : 'no-verify'}>

                <div style={{
                    fontWeight: '500',
                    margin: 'auto',
                    textAlign: 'center',
                    color: 'yellow'
                }}>
                    <Image src={user.avatar}
                        roundedCircle
                        className="avatar-user-item"
                        style={{
                            margin: 'auto'
                        }}
                    />
                    {user.userName}<br />
                    {(user.authorize.role == role.Admin.role) && (<div style={{ color: 'rgb(255, 48, 48)', fontWeight: '700' }}>{user.authorize.name}</div>)}
                    {(user.authorize.role == role.Manager.role) && (<div style={{ color: 'aqua', fontWeight: '700' }}>{user.authorize.name}</div>)}
                    {(user.authorize.role == role.User.role) && (<div style={{ color: 'rgb(79, 250, 57)', fontWeight: '700' }}>{user.authorize.name}</div>)}
                </div>
            </div>
            <div>
                <Tabs
                    defaultActiveKey="profile"
                    id="justify-tab-example"
                    className="mb-3"
                    justify
                >
                    <Tab eventKey="profile" title="Profile" style={{ background: 'lightgray' }}>
                        <div className='tab-user'>
                            <div className='user-profile'>
                                <label><i class="fa-solid fa-address-card"></i>Full Name: <span>{user.fullName}</span></label><br />
                                <label><i class="bi bi-envelope-at-fill"></i>Email: <span>{user.email}</span></label><br />
                                <label><i class="bi bi-calendar"></i>Create Date: <span>{user.createDate}</span></label><br />
                                {(user.authorize.role == role.Manager.role) && (<><label><i class="bi bi-envelope-at-fill"></i>  Faculty: <span>{user.faculty}</span></label><br /></>)}
                            </div>
                            <div className='user-authorize'>
                                {(user.authorize.role == role.Admin.role) && (<div style={{ color: 'red', fontWeight: '900', margin: 'auto', fontSize: '20px' }}>This is a {role.Admin.name} so you cannot change their role!</div>)}
                                {(user.authorize.role == role.Manager.role) && (<>
                                    <h3 style={{ margin: 'auto' }}>Update user to {role.User.name}</h3>
                                    <div className='set-role-btn' onClick={handleShow}>
                                        <i class="bi bi-person-lines-fill"></i>
                                    </div>
                                    <Modal show={showVerify} onHide={handleClose} centered>
                                        <Modal.Body>Are you sure about that ?</Modal.Body>
                                        <Modal.Footer>
                                            <Button variant="secondary" onClick={handleClose}>
                                                Cancel
                                            </Button>
                                            <Button variant="primary" onClick={() => {
                                                changeUser(user.id)
                                                handleClose()
                                            }}>
                                                Save Changes
                                            </Button>
                                        </Modal.Footer>
                                    </Modal>
                                </>)}
                                {(user.authorize.role == role.User.role) && (<>
                                    <h3 style={{ margin: 'auto' }}>Update user to {role.Manager.name}</h3>
                                    <div className='set-role-btn' onClick={handleShow}>
                                        <i class="bi bi-person-fill"></i>
                                    </div>
                                    <Modal show={showVerify} onHide={handleClose} centered>
                                        <Modal.Header style={{ display: 'flex', justifyContent: 'center', fontWeight: '900', fontSize: '30px' }}>
                                            Choose one Faculty
                                        </Modal.Header>
                                        <Modal.Body>
                                            <Form.Select value={selectedFaculty} onChange={handleSelectChange}>
                                                <option value="">---Select a faculty---</option>
                                                {listFacultyOption && listFacultyOption.map((option, index) => (
                                                    <option key={index} value={option.id}>{option.name}</option>
                                                ))}
                                            </Form.Select>
                                        </Modal.Body>
                                        <Modal.Footer>
                                            <Button variant="secondary" onClick={handleClose}>
                                                Cancel
                                            </Button>
                                            <Button variant="primary"
                                                onClick={() => {
                                                    changeManager(user.id)
                                                    handleClose()
                                                }}
                                                disabled={(selectedFaculty && selectedFaculty !== '') ? false : true}
                                            >
                                                Save Changes
                                            </Button>
                                        </Modal.Footer>
                                    </Modal>
                                </>)}
                            </div>
                        </div>
                    </Tab>
                    {(user.authorize.role == role.User.role) && (
                        <Tab eventKey="post" title="Post" style={{ background: 'lightgray' }}>
                            <div className='tab-user'>

                            </div>
                        </Tab>
                    )}
                </Tabs>
            </div>
        </>)}

    </>)
}

export default UserDetail