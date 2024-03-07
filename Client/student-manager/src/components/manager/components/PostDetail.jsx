import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { toast } from 'react-toastify';
import { GetPostManagerDetail, UpdateAndApprovedPost } from '../../../api/services/PostService';
import { Image } from 'react-bootstrap';
import DocxLogo from '../../../assets/images/docx-logo.png'
import { Modal, Button, Form } from 'react-bootstrap';


const PostDetail = () => {
    let { postId } = useParams();
    const [post, setPost] = useState()
    const [postUpdate, setPostUpdate] = useState({
        id: postId,
        title: '',
        isApproved: false,
        avatarPost: null,
        linkDocument: null
    })

    const updatePostFields = (updatedFields) => {
        setPost(prevPost => ({
            ...prevPost,
            ...updatedFields
        }));
    };

    const [show, setShow] = useState(false);

    const handleInputChange = (e) => {
        const { name, value, type, checked, files } = e.target;
        const inputValue = type === 'checkbox' ? checked : type === 'file' ? files[0] : value;
        setPostUpdate({ ...postUpdate, [name]: inputValue });
    };

    const handleClose = () => setShow(false);

    const handleShow = () => {
        setPostUpdate({
            ...postUpdate,
            title: post.title,
            isApproved: post.isApproved
        })
        setShow(true)
    };

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

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const res = await UpdateAndApprovedPost(postUpdate)
            if (res.State === 1) {
                updatePostFields(res.Data)
                toast.success("Approved successful!")
            } else {
                toast.warning(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
        handleClose()
    };

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
                    <Button variant="primary" onClick={handleShow} style={{
                        marginLeft: '60%',
                        marginTop: '20%'
                    }}>
                        Update post
                    </Button>
                </div>

                <Modal show={show} onHide={handleClose}>
                    <Form onSubmit={handleSubmit}>
                        <Modal.Header closeButton>
                            <Modal.Title>Update Post</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form.Group>
                                <Form.Label>Title:</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="title"
                                    value={postUpdate.title}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group>
                                <Form.Check
                                    type="checkbox"
                                    label="Approved"
                                    name="isApproved"
                                    checked={postUpdate.isApproved}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group>
                                <Form.Label>Avatar Post:</Form.Label>
                                <Form.Control
                                    type="file"
                                    name="avatarPost"
                                    accept="image/*"
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group>
                                <Form.Label>Link Document:</Form.Label>
                                <Form.Control
                                    type="file"
                                    name="linkDocument"
                                    accept=".docx"
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={handleClose}>
                                Close
                            </Button>
                            <Button variant="primary" type="submit">
                                Update Post
                            </Button>
                        </Modal.Footer>
                    </Form>
                </Modal>
            </div>
            <div className='content'>
                <div dangerouslySetInnerHTML={{ __html: post.content }} />
            </div>
        </div>)}

    </>)
}

export default PostDetail