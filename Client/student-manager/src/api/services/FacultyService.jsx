import Root from '../Root';

const adminController = `admin`
const userController = `user`

const CreateFaculty = (props) =>{
    return Root.post(`${adminController}/add-faculty`,{
        name: props.name
    })
}

const DeleteFaculty = (id) =>{
    return Root.delete(`${adminController}/del-faculty/?idFaculty=${id}`)
}

const RestoreFaculty = (id) =>{
    return Root.put(`${adminController}/restore-faculty/?idFaculty=${id}`)
}

const UpdateFaculty = (props) => {
    return Root.put(`${adminController}/update-faculty`,{
        id: props.id.toString(),
        name: props.name.toString()
    })
}

const GetAllFaculty = () =>{
    return Root.get(`${adminController}/get-all-faculty`)
}

export {
    CreateFaculty,
    DeleteFaculty,
    RestoreFaculty,
    UpdateFaculty,
    GetAllFaculty
}