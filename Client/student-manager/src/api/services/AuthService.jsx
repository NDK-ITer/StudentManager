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

const ChangePassword = (props) =>{
    return Root.post(`${controllerName}/change-password`,{
        newPassword: props.password
    })
}

const UpdateProfile = (props) =>{
    return Root.post(`${controllerName}/edit-profile`,{
        firstName: props.firstName,
        lastName: props.lastName,
        userName: props.userName
    })
}

export {
    Login,
    Register,
    GetMyInformation,
    UploadAvatar,
    ChangePassword,
    UpdateProfile
}