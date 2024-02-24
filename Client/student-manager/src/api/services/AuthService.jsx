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

const UploadAvatar = (props) =>{
    const formData = new FormData();
    formData.append('newAvatar', props.uploadAvatar);
    return Root.post(`${controllerName}/edit-avatar`,formData)
}

export {
    Login,
    Register,
    GetMyInformation,
    UploadAvatar
}