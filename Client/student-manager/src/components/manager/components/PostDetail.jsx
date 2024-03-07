import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import { GetPostManagerDetail } from '../../../api/services/PostService';
import { Image } from 'react-bootstrap';
import DocxLogo from '../../../assets/images/docx-logo.png'


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
                <div className='avatar-post'>
                    <Image src={post.avatarPost} />
                </div>
                <div className='information'>
                    <span className='title'>{post.title}</span>
                    <div className='link-doc'>
                        Get content    <i class="fa-solid fa-hand-point-right"></i>
                        <a href={post.linkDocument}>
                            <Image src={DocxLogo} />
                        </a>
                    </div>
                </div>
            </div>
            <div className='content'>
                <div dangerouslySetInnerHTML={{ __html: post.content }} />
            </div>
        </div>)}

    </>)
}

export default PostDetail