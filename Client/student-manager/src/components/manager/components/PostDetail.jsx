import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import { GetPostManagerDetail } from '../../../api/services/PostService';


const PostDetail = () => {
    let { postId } = useParams();
    const [post, setPost] = useState()

    const getPostDetail = async () => {
        try {
            const res = await GetPostManagerDetail(postId)
            if (res.State === 1) {
                setPost(res.Data)
            } else {
                toast.warning(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    useEffect(() => {
        getPostDetail()
    }, [])
    return (<>
        {post && (<div className='manager-post-detail-container'>
            <div className='information-post'>

            </div>
            <div className='content'>
                <div dangerouslySetInnerHTML={{ __html: post.content }} />
            </div>
        </div>)}

    </>)
}

export default PostDetail