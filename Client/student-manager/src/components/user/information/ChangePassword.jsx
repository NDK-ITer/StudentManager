import { useState } from "react";
import Modal from 'react-bootstrap/Modal';
import { Form, Button } from 'react-bootstrap';
import { toast } from "react-toastify";
import { ChangePassword } from "../../../api/services/AuthService";

const ChangePasswordForm = ({ show, handleClose }) => {

    const [changePasswordData, seChangePasswordData] = useState({
        password: '',
        confirmPassword: '',
    })
    const [passwordsMatch, setPasswordsMatch] = useState(true);
    const [isShowPassword, setIsShowPassword] = useState(false)
    const [loadingAPI, setLoadingAPI] = useState(false)

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        seChangePasswordData({
            ...changePasswordData,
            [name]: value,
        });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            if (changePasswordData.confirmPassword !== changePasswordData.password) {
                setPasswordsMatch(false);
                return
            } else {
                setPasswordsMatch(true);
            }
            setLoadingAPI(true)
            const res = await ChangePassword(changePasswordData)
            if (res.State === 1) {
                toast.success(res.Data.mess)
            } else {
                toast.error(res.Data.mess)
            }
            setLoadingAPI(false)
            handleClose()
        } catch (error) {
            toast.error('Register error!');
            setLoadingAPI(false)
        }
    }

    return (<>
        <div>
            <Modal show={show} onHide={handleClose} centered>
                <Form onSubmit={handleSubmit}>
                    <Modal.Header closeButton>
                        <Modal.Title centered>Change your password</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form.Group controlId="formBasicPassword">
                            <Form.Label>New Password:</Form.Label>
                            <Form.Control
                                type={isShowPassword === true ? 'text' : 'password'}
                                name="password"
                                value={changePasswordData.password}
                                onChange={handleInputChange}
                                placeholder="password"
                            />
                            <div className='show-password'>
                                <i className={isShowPassword === true ? "fa-solid fa-eye" : "fa-solid fa-eye-slash"}
                                    onClick={() => setIsShowPassword(!isShowPassword)}></i>
                            </div>
                        </Form.Group>

                        <Form.Group controlId="formBasicConfirmPassword">
                            <Form.Label>Confirm New Password:</Form.Label>
                            <Form.Control
                                type={isShowPassword === true ? 'text' : 'password'}
                                name="confirmPassword"
                                value={changePasswordData.confirmPassword}
                                onChange={handleInputChange}
                                placeholder="confirm password"
                                isInvalid={!passwordsMatch}
                            />
                            <Form.Control.Feedback type="invalid">
                                Passwords is not confirm
                            </Form.Control.Feedback>
                        </Form.Group>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button
                            variant="primary"
                            type="submit"
                            disabled={(changePasswordData.confirmPassword && changePasswordData.password) || loadingAPI ? false : true}
                        >
                            {loadingAPI && (<i className="fa-solid fa-sync fa-spin"></i>)} Save Changes
                        </Button>
                    </Modal.Footer>
                </Form>
            </Modal>
        </div>
    </>)
}

export default ChangePasswordForm