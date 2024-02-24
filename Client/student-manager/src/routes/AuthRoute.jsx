import {Link, useNavigate} from 'react-router-dom';
import { useContext } from 'react';
import { UserContext } from '../contexts/UserContext';
import Alert from 'react-bootstrap/Alert';

const AuthRoute = (props) => {
    const {user} = useContext(UserContext)
    let navigate = useNavigate()
    if (user && !user.isAuth) {
        navigate('/auth')
        return<>
            <Alert variant="danger" className='mt-3'>
                <Alert.Heading>Login please!</Alert.Heading>
                    <p>
                        <Link to='/auth'>Click here</Link> to login
                    </p>
            </Alert>
        </>
    }
    return(<>
        {props.children}
    </>)
}

export default AuthRoute