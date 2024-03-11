import { useEffect, useState } from "react"
import { useParams } from "react-router-dom"
import { toast } from "react-toastify"
import { GetPublicPostById } from '../../api/services/PostService'
import { Container } from "react-bootstrap"

const DetailPost = () => {
    const { postId } = useParams()
    const [post, setPost] = useState()

    const getPostById = async () => {
        try {
            const res = await GetPublicPostById(postId)
            if (res.State === 1) {
                setPost(res.Data)
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
                        fontSize:'200%',
                        fontWeight:'bolder'
                    }}>{post.title}</div>
                    <div>
                        Posted at: {post.uploadDate}
                    </div>
                    <div dangerouslySetInnerHTML={{ __html: post.content }} />
                </div>
                <div className="comment-post">
                    
                </div>
            </>)}
        </div>
    </>)
}

export default DetailPost