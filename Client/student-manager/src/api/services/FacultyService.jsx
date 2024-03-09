import Root from '../Root';

const adminController = `admin`
const userController = `user`
const managerController = `manager`


const CreateFaculty = (props) => {
    return Root.post(`${adminController}/add-faculty`, {
        name: props.name
    })
}

const DeleteFaculty = (id) => {
    return Root.delete(`${adminController}/del-faculty/?idFaculty=${id}`)
}

const RestoreFaculty = (id) => {
    return Root.put(`${adminController}/restore-faculty/?idFaculty=${id}`)
}

const UpdateFaculty = (props) => {
    return Root.put(`${adminController}/update-faculty`, {
        id: props.id.toString(),
        name: props.name.toString()
    })
}

const GetAllFaculty = () => {
    return Root.get(`${adminController}/get-all-faculty`)
}

const GetFacultyPublic = () => {
    return Root.get(`${userController}/get-faculty-public`)
}

const GetFacultyById = (idFaculty) => {
    return Root.get(`${managerController}/get-faculty/?idFaculty=${idFaculty}`)
}

const SetStateFaculty = (idFaculty) =>{
    return Root.put(`${managerController}/state-faculty/?idFaculty=${idFaculty}`)
}

export {
    CreateFaculty,
    DeleteFaculty,
    RestoreFaculty,
    UpdateFaculty,
    GetAllFaculty,
    GetFacultyPublic,
    GetFacultyById,
    SetStateFaculty
}