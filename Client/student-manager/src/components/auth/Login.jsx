import { useContext, useEffect, useState } from 'react';
import { Form, Button } from 'react-bootstrap';
import Cookies from 'js-cookie';
import { Login } from '../../api/services/AuthService';
import { toast } from "react-toastify";
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../../contexts/UserContext';

const LoginForm = (props) => {

    let navigate = useNavigate()
    const { loginContext } = useContext(UserContext)
    const [loadingAPI, setLoadingAPI] = useState(false)
    const [isShowPassword, setIsShowPassword] = useState(false)
    const [loginData, setLoginData] = useState({
        email: '',
        password: '',
    });

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setLoginData({
            ...loginData,
            [name]: value,
        });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            if (!loginData.email) {
                toast.error(`email is required!`)
                return
            }
            if (!loginData.password) {
                toast.error(`password is required!`)
                return
            }
            setLoadingAPI(true)
            const response = await Login(loginData)
            if (response.State === 1) {
                toast.success(`Login is successful!`);
                loginContext(response)
                navigate('/')
            }
            else {
                toast.error(response.Data.mess);
            }
            setLoadingAPI(false)
        } catch (error) {
            toast.error('Login error');
            setLoadingAPI(false)
        }
    };
    useEffect(() => {
        const jwt = Cookies.get('jwt');
        if (jwt) {
            navigate('/')
        }
    }, [])

    return (
        <div className='login-form'>
            <Form onSubmit={handleSubmit}>
                <Form.Group className='form-controller-custom' controlId="formBasicEmail">
                    <Form.Label></Form.Label>
                    <Form.Control
                        type="email"
                        name="email"
                        value={loginData.email}
                        onChange={handleInputChange}
                        placeholder="email"
                    />
                </Form.Group>

                <Form.Group className='form-controller-custom' controlId="formBasicPassword">
                    <Form.Label></Form.Label>
                    <Form.Control
                        type={isShowPassword === true ? 'text' : 'password'}
                        name="password"
                        value={loginData.password}
                        onChange={handleInputChange}
                        placeholder="password"
                    />
                    <div className='show-password'>
                        <i className={isShowPassword === true ? "fa-solid fa-eye" : "fa-solid fa-eye-slash"}
                            onClick={() => setIsShowPassword(!isShowPassword)}></i>
                    </div>
                </Form.Group>

                <Form.Group>
                    <Button variant="primary" type="submit" className='btn-submit'
                        disabled={(loginData.email && loginData.password) || loadingAPI ? false : true}
                    >
                        {loadingAPI && (<i className="fa-solid fa-sync fa-spin"></i>)}
                        &nbsp;Login
                    </Button>
                </Form.Group>
            </Form>
        </div>

    );
}

export default LoginForm;
