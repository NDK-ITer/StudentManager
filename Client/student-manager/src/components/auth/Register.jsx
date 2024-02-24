import React, { useContext, useState } from 'react';
import { Form, Button } from 'react-bootstrap';
import Cookies from 'js-cookie';
import {Register} from '../../api/services/AuthService';
import { ToastContainer, toast } from "react-toastify";
import { UserContext } from '../../contexts/UserContext';


const RegisterForm = () => {
    const {loginContext} = useContext(UserContext)
    const [loadingAPI, setLoadingAPI] = useState(false)
    const [registerData, setRegisterData] = useState({
        firstName: '',
        lastName: '',
        userName:'',
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
                toast.error('Password is not confirm!')
                return
            }
            setLoadingAPI(true)
            const response = await Register(registerData)
            if (response.state === 1) {
                toast.success('Join successful!')
                loginContext(response)
                console.log(Cookies.get('jwt'))
            }
            setLoadingAPI(false)
        } catch (error) {
            console.error('Register error:', error);
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
            name="specialName"
            value={registerData.specialName}
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
            type="password"
            name="password"
            value={registerData.password}
            onChange={handleInputChange}
            placeholder="password"
            />
        </Form.Group>

        <Form.Group controlId="formBasicConfirmPassword">
            <Form.Label>Confirm Password:</Form.Label>
            <Form.Control
            type="password"
            name="confirmPassword"
            value={registerData.confirmPassword}
            onChange={handleInputChange}
            placeholder="confirm password"
            />
        </Form.Group>

        <Form.Group>
            <Button variant="primary" type="submit" className='btn-submit'>
                {loadingAPI && (<i class="fa-solid fa-sync fa-spin"></i>)}Sign up
            </Button>
            <ToastContainer />
        </Form.Group>
        </Form>
    );
}

export default RegisterForm;
