import Root from '../Root';
import Cookies from 'js-cookie';

const controllerName = `auth`

const Login = (props) =>{
    return Root.post(`${controllerName}/login`,{
        email: props.email,
        password: props.password
    })
}

const Register = (props) =>{
    return Root.post(`${controllerName}/register`,{
        firstName: props.firstName,
        lastName: props.lastName,
        userName: props.userName,
        email: props.email,
        password: props.password
    })
}

const Logout = () =>{
    Cookies.set('jwt','')
    Cookies.set('user','')
}

export {
    Login,
    Register
}