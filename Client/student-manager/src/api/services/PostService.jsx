import Root from '../Root';

const adminController = `admin`
const userController = `user`

const UploadPost = (props) =>{
    const formData = new FormData();
    formData.append('avatarPost', props.avatarPost);
    formData.append('document', props.avatarPost);
    return Root.post(`${userController}/upload-post`,{
        facultyId: props.facultyId,
        title: props.title
    })
}

const GetMyPost = () => {
    return Root.get(`${userController}/get-my-post`)
}

export {
    UploadPost,
    GetMyPost
}