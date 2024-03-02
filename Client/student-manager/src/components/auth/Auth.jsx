import React, { useState } from 'react';
import { Container, Row, Col, Button } from 'react-bootstrap';
import LoginForm from './Login';
import RegisterForm from './Register';


function Auth() {
    const [isLoginForm, setIsLoginForm] = useState(true);

    const handleFormSwitch = () => {
        setIsLoginForm(!isLoginForm);
    };

    return (
        <Container >
            <div className='auth-form'>
                <div>
                    <Row className="justify-content-md-center">
                        <Col xs={12} md={6}>
                            <h1 className="text-center">{isLoginForm ? 'Log in' : 'Welcome'}</h1>
                            {isLoginForm ? (
                                <LoginForm />
                            ) : (
                                <RegisterForm toLogin={setIsLoginForm} />
                            )}
                            <p className="text-center mt-3">
                                {isLoginForm ? 'Not available account?' : 'available account?'}
                                <Button variant="link" onClick={handleFormSwitch} className="ml-2">
                                    {isLoginForm ? 'Sign in now!' : 'Sign up now!'}
                                </Button>
                            </p>
                        </Col>
                    </Row>
                </div>
            </div>
        </Container>
    );
}

export default Auth;
