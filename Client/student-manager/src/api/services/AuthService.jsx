import Root from '../Root';

const AuthController = `auth`

const Login = (props) =>{
    return Root.post(`${AuthController}/login`,{
        email: props.email,
        password: props.password
    })
}

const Register = (props) =>{
    return Root.post(`${AuthController}/register`,{
        firstName: props.firstName,
        lastName: props.lastName,
        userName: props.userName,
        email: props.email,
        password: props.password
    })
}

const GetMyInformation = () =>{
    return Root.get(`${AuthController}/information`)
}

const UploadAvatar = (props) =>{
    const formData = new FormData();
    formData.append('newAvatar', props.uploadAvatar);
    return Root.post(`${AuthController}/edit-avatar`,formData)
}

const ChangePassword = (props) =>{
    return Root.post(`${AuthController}/change-password`,{
        newPassword: props.password
    })
}

const UpdateProfile = (props) =>{
    return Root.post(`${AuthController}/edit-profile`,{
        firstName: props.firstName,
        lastName: props.lastName,
        userName: props.userName
    })
}

const GetAllRole = () =>{
    return Root.get(`${AuthController}/get-all-role`)
}

export {
    Login,
    Register,
    GetMyInformation,
    UploadAvatar,
    ChangePassword,
    UpdateProfile,
    GetAllRole
}