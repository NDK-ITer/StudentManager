import {Link, useNavigate} from 'react-router-dom';
import { useContext } from 'react';
import { UserContext } from '../contexts/UserContext';
import { RoleContext } from '../contexts/RoleContext';
import Alert from 'react-bootstrap/Alert';

const CheckStaffRoute = (props) => {
    const {role} = useContext(RoleContext)
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
    if (user.role != role.Staff.role){
        return<>
            <Alert variant="warning" className='mt-3'>
                <Alert.Heading>You are not authorized to enter here!</Alert.Heading>
            </Alert>
        </>
    }
    return(<>
        {props.children}
    </>)
}

export default CheckStaffRoute