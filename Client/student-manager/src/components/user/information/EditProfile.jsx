
import { useContext, useState } from 'react';
import Modal from 'react-bootstrap/Modal';
import { Form, Button } from 'react-bootstrap';
import { toast } from 'react-toastify';
import { UpdateProfile } from '../../../api/services/AuthService';
import { UserContext } from '../../../contexts/UserContext';

const EditProfile = ({ show, onClose, data }) => {

    const {UpdateUser} = useContext(UserContext)
    const [loadingAPI, setLoadingAPI] = useState(false)
    const [editData, setEditData] = useState({
        firstName: data.firstName,
        lastName: data.lastName,
        userName: data.userName,
    });

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setEditData({
            ...editData,
            [name]: value,
        });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            setLoadingAPI(true)
            const res = await UpdateProfile(editData)
            if (res.State === 1) {
                toast.success(res.Data.mess)
                UpdateUser({
                    userName: res.Data.userName,
                    firstName: res.Data.firstName,
                    lastName: res.Data.lastName,
                })
            } else {
                toast.error(res.Data.mess)
            }
            setLoadingAPI(false)
        } catch (error) {
            toast.error('Register error!');
            setLoadingAPI(false)
        }
    };

    return (<>
        <div>
            <Modal show={show} onHide={onClose} centered>
                <Form onSubmit={handleSubmit}>
                    <Modal.Header closeButton>
                        <Modal.Title>Update Profile</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form.Group controlId="formBasicName">
                            <Form.Label>First Name:</Form.Label>
                            <Form.Control
                                type="text"
                                name="firstName"
                                value={editData.firstName}
                                onChange={handleInputChange}
                                placeholder="first name"
                            />
                        </Form.Group>

                        <Form.Group controlId="formBasicName">
                            <Form.Label>Last Name:</Form.Label>
                            <Form.Control
                                type="text"
                                name="lastName"
                                value={editData.lastName}
                                onChange={handleInputChange}
                                placeholder="last name"
                            />
                        </Form.Group>

                        <Form.Group controlId="formBasicName">
                            <Form.Label>User Name:</Form.Label>
                            <Form.Control
                                type="text"
                                name="userName"
                                value={editData.userName}
                                onChange={handleInputChange}
                                placeholder="user name"
                            />
                        </Form.Group>

                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="primary"
                            onClick={onClose}
                            type='submit'
                            disabled={(editData.firstName && editData.lastName && editData.userName) || loadingAPI ? false : true}
                        >
                            {loadingAPI && (<i class="fa-solid fa-sync fa-spin"></i>)} Save Changes
                        </Button>
                    </Modal.Footer>
                </Form>
            </Modal>
        </div>
    </>)
}

export default EditProfile