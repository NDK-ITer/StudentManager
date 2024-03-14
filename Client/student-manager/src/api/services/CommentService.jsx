import Root from '../Root';

const userController = 'user'

const CommentPost = (props) =>{
    return Root.post(`${userController}/comment`,{
        idPost: props.idPost,
        content: props.content
    })
}

export {
    CommentPost
}