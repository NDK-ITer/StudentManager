import Root from '../Root';

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

const GetMyInformation = () =>{
    return Root.get(`${controllerName}/information`)
}

export {
    Login,
    Register,
    GetMyInformation
}