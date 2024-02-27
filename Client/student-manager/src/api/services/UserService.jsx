import Root from '../Root';

const adminController = `admin`

const GetAllUser = () =>{
    return Root.get(`${adminController}/get-all-user`)
}

const SetLockUser = (props) =>{
    return Root.put(`${adminController}/set-lock/?idUserIsLocked=${props.idUser}`,)
}

const GetAllPost = () =>{
    return Root.get(`${adminController}/get-all-post`)
}

export {
    GetAllUser,
    SetLockUser,
    GetAllPost
}