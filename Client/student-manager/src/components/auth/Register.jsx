import React, { useState } from 'react';
import { Form, Button } from 'react-bootstrap';
import { Register } from '../../api/services/AuthService';
import { toast } from "react-toastify";


const RegisterForm = () => {
    const [passwordsMatch, setPasswordsMatch] = useState(true);
    const [loadingAPI, setLoadingAPI] = useState(false)
    const [isShowPassword, setIsShowPassword] = useState(false)
    const [registerData, setRegisterData] = useState({
        firstName: '',
        lastName: '',
        userName: '',
        email: '',
        password: '',
        confirmPassword: '',
    });

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setRegisterData({
            ...registerData,
            [name]: value,
        });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            if (registerData.confirmPassword !== registerData.password) {
                setPasswordsMatch(false);
                return
            } else {
                setPasswordsMatch(true);
            }
            setLoadingAPI(true)
            const response = await Register(registerData)
            if (response.State === 1) {
                toast.success(response.Data.mess)
            }else{
                toast.error(response.Data.mess)
            }
            setLoadingAPI(false)
        } catch (error) {
            toast.error('Register error!');
            setLoadingAPI(false)
        }
    };

    return (
        <Form onSubmit={handleSubmit}>
            <Form.Group controlId="formBasicName">
                <Form.Label>First Name:</Form.Label>
                <Form.Control
                    type="text"
                    name="firstName"
                    value={registerData.firstName}
                    onChange={handleInputChange}
                    placeholder="first name"
                />
            </Form.Group>

            <Form.Group controlId="formBasicName">
                <Form.Label>Last Name:</Form.Label>
                <Form.Control
                    type="text"
                    name="lastName"
                    value={registerData.lastName}
                    onChange={handleInputChange}
                    placeholder="last name"
                />
            </Form.Group>

            <Form.Group controlId="formBasicName">
                <Form.Label>User Name:</Form.Label>
                <Form.Control
                    type="text"
                    name="userName"
                    value={registerData.userName}
                    onChange={handleInputChange}
                    placeholder="user name"
                />
            </Form.Group>

            <Form.Group controlId="formBasicEmail">
                <Form.Label>Email:</Form.Label>
                <Form.Control
                    type="email"
                    name="email"
                    value={registerData.email}
                    onChange={handleInputChange}
                    placeholder="email"
                />
            </Form.Group>

            <Form.Group controlId="formBasicPassword">
                <Form.Label>Password:</Form.Label>
                <Form.Control
                    type={isShowPassword === true ? 'text' : 'password'}
                    name="password"
                    value={registerData.password}
                    onChange={handleInputChange}
                    placeholder="password"
                />
                <div className='show-password'>
                    <i className={isShowPassword === true ? "fa-solid fa-eye" : "fa-solid fa-eye-slash"}
                        onClick={() => setIsShowPassword(!isShowPassword)}></i>
                </div>
            </Form.Group>

            <Form.Group controlId="formBasicConfirmPassword">
                <Form.Label>Confirm Password:</Form.Label>
                <Form.Control
                    type={isShowPassword === true ? 'text' : 'password'}
                    name="confirmPassword"
                    value={registerData.confirmPassword}
                    onChange={handleInputChange}
                    placeholder="confirm password"
                    isInvalid={!passwordsMatch}
                />
                <Form.Control.Feedback type="invalid">
                    Passwords is not confirm
                </Form.Control.Feedback>
            </Form.Group>

            <Form.Group>
                <Button variant="primary" type="submit" className='btn-submit'>
                    {loadingAPI && (<i class="fa-solid fa-sync fa-spin"></i>)} Sign up
                </Button>
            </Form.Group>
        </Form>
    );
}

export default RegisterForm;
