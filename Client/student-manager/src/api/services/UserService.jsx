import Root from '../Root';

const adminController = `admin`
const userController = `user`

const GetAllUser = () =>{
    return Root.get(`${adminController}/get-all-user`)
}

const SetLockUser = (props) =>{
    return Root.put(`${adminController}/set-lock/?idUserIsLocked=${props.idUser}`,)
}

const GetAllPost = () =>{
    return Root.get(`${adminController}/get-all-post`)
}

const GetUserById = (props) => {
    return Root.get(`${userController}/${props.id}`)
}

const SetUser = (idUserSet) =>{
    return Root.put(`${adminController}/set-user/?idUserSet=${idUserSet}`)
}

const SetManager = (idUserSet) =>{
    return Root.put(`${adminController}/set-manager/?idUserSet=${idUserSet}`)
}

export {
    GetAllUser,
    SetLockUser,
    GetAllPost,
    GetUserById,
    SetUser,
    SetManager,
}