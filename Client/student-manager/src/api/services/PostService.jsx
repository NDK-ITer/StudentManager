import Root from '../Root';

const adminController = `admin`
const userController = `user`
const managerController = `manager`

const UploadPost = (props) =>{
    console.log(props)
    const formData = new FormData();
    formData.append('AvatarPost', props.avatarPost);
    formData.append('Document', props.document);
    formData.append('FacultyId',props.facultyId)
    formData.append('Title',props.title)
    return Root.post(`${userController}/upload-post`,formData)
}

const GetMyPost = () => {
    return Root.get(`${userController}/get-my-post`)
}

const GetPostOfFaculty = (idFaculty) =>{
    return Root.get(`${managerController}/get-post-faculty/?idFaculty=${idFaculty}`)
}

const GetPostManagerDetail = (idPost) =>{
    return Root.get(`${managerController}/get-post-faculty/${idPost}`)
}

export {
    UploadPost,
    GetMyPost,
    GetPostOfFaculty,
    GetPostManagerDetail
}