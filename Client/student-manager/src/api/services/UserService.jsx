import Root from '../Root';

const adminController = `admin`
const userController = `user`
const staffController = `staff`

const GetAllUser = () =>{
    return Root.get(`${adminController}/get-all-user`)
}

const GetListUser = () =>{
    return Root.get(`${staffController}/get-list-user`)
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

const SetStudent = (idUserSet) =>{
    return Root.put(`${adminController}/set-student/?idUserSet=${idUserSet}`)
}

const SetStaff = (idUserSet) =>{
    return Root.put(`${adminController}/set-staff/?idUserSet=${idUserSet}`)
}

const SetUser = (idUserSet) =>{
    return Root.put(`${adminController}/set-user/?idUserSet=${idUserSet}`)
}

const SetManager = (idUserSet, idFaculty) =>{
    return Root.put(`${adminController}/set-manager/?idUserSet=${idUserSet}&idFaculty=${idFaculty}`)
}

export {
    GetAllUser,
    SetLockUser,
    GetAllPost,
    GetUserById,
    SetUser,
    SetManager,
    GetListUser,
    SetStudent,
    SetStaff,
}