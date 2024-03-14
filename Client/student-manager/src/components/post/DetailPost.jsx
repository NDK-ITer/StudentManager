import { useEffect, useState } from "react"
import { useParams } from "react-router-dom"
import { toast } from "react-toastify"
import { GetPublicPostById } from '../../api/services/PostService'
import { CommentPost } from '../../api/services/CommentService'
import { Form, Image } from "react-bootstrap"

const DetailPost = () => {
    const { postId } = useParams()
    const [post, setPost] = useState()
    const [listComment, setListComment] = useState([])
    const [commentData, setCommentData] = useState('')

    const addCommentToList = (props) => {
        setListComment(pre => [props, ...pre])
    }

    const commentPost = async () => {
        try {
            const res = await CommentPost({
                idPost: postId,
                content: commentData
            })
            if (res.State === 1) {
                await addCommentToList(res.Data)
            }
            else {
                toast.warning(res.Data.mess)
            }
            setCommentData('')
        } catch (error) {
            toast.error(error)
        }
    }

    const getPostById = async () => {
        try {
            const res = await GetPublicPostById(postId)
            if (res.State === 1) {
                setPost(res.Data)
                setListComment(res.Data.listComment)
            }
            else {
                toast.warning(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    useEffect(() => {
        getPostById()
    }, [])

    return (<>
        <div className="detail-post-public">
            {post && (<>
                <div className="content" >
                    <div style={{
                        fontSize: '200%',
                        fontWeight: 'bolder'
                    }}>{post.title}</div>
                    <div>
                        Posted at: {post.uploadDate}
                    </div>
                    <div dangerouslySetInnerHTML={{ __html: post.content }} />
                </div>
                <div className="comment-post">
                    <div className="list-comment">
                        {listComment && listComment.length > 0 && listComment.map((item, index) => {
                            return (<>
                                <div className="item">
                                    <div className="avatar-user">
                                        <Image src={item.avatarUser} style={{width: '100%', height: '100%',objectFit: 'cover' }} roundedCircle />
                                    </div>
                                    <div className="cmt-detail">
                                        <div className="content-cmt">
                                            {item.content}
                                        </div>
                                        <div className="date-cmt">
                                            Date comment: {item.dateComment}
                                        </div>
                                    </div>
                                </div>
                            </>)
                        })}
                    </div>

                    <div className="input-comment">
                        <Form style={{
                            width: '80%',
                            marginLeft: '10%'
                        }}>
                            <Form.Control
                                type="text"
                                placeholder="Comment"
                                value={commentData}
                                onChange={(event) => setCommentData(event.target.value)}
                            />
                        </Form>
                        <div className="btn-post-cmt" onClick={commentPost}><i class="fa-solid fa-paper-plane" style={{ margin: 'auto', color: 'white' }}></i></div>
                    </div>
                </div>
            </>)}
        </div>
    </>)
}

export default DetailPost