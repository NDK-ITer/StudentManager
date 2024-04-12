import Root from '../Root';

const adminController = `admin`
const userController = `user`
const managerController = `manager`

const UploadPost = (props) =>{
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

const UpdateAndApprovedPost = (props) =>{
    const formData = new FormData();
    formData.append('Id', props.id);
    formData.append('Title', props.title);
    formData.append('AvatarPost',props.avatarPost)
    formData.append('LinkDocument',props.linkDocument)
    formData.append('IsApproved',props.isApproved)
    return Root.put(`${managerController}/approved-post`,formData)
}

const GetPostPublic = () =>{
    return Root.get(`${userController}/get-post-public`)
}

const GetPublicPostById = (idPost) =>{
    return Root.get(`${userController}/get-post/?idPost=${idPost}`)
}

const GetDateValue = () => {
    return Root.get(`${adminController}/get-date-value`)
}

const GetReportFaculty = (facultyId, fromYear, toYear) => {
    console.log(fromYear, toYear)
    return Root.get(`${managerController}/get-report?facultyId=${facultyId}&fromYear=${fromYear}&toYear=${toYear}`)
}

const GetAllPost = () => {
    return Root.get(`${adminController}/get-all-post`)
}

export {
    UploadPost,
    GetMyPost,
    GetPostOfFaculty,
    GetPostManagerDetail,
    UpdateAndApprovedPost,
    GetPostPublic,
    GetPublicPostById,
    GetDateValue,
    GetReportFaculty,
    GetAllPost
}