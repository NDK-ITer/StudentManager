import { useEffect, useState } from "react"
import { UploadPost, GetMyPost } from "../../../api/services/PostService"
import { GetFacultyPublic } from '../../../api/services/FacultyService'
import { toast } from "react-toastify"
import { Button, Form, Image, Modal } from 'react-bootstrap'
import DefaultImg from '../../../assets/images/default-img.png'

const Post = ({ userId }) => {

    const [listPost, setListPost] = useState([])
    const [postData, setPostData] = useState({
        facultyId: '',
        title: '',
        avatarPost: null,
        document: null,
    })
    const [listFaculty, setListFaculty] = useState([])
    const [show, setShow] = useState(false);

    const handleChange = (e) => {
        const { name, value, files } = e.target;
        setPostData({
            ...postData,
            [name]: files ? files[0] : value,
        });
    };

    const handleFacultyChange = (e) => {
        setPostData({
            ...postData,
            facultyId: e.target.value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        uploadPost();
    };

    const handleClose = () => setShow(false);

    const handleShow = () => setShow(true);

    const [showApprovedOnly, setShowApprovedOnly] = useState(false);

    const handleCheckboxChange = () => {
        setShowApprovedOnly(!showApprovedOnly);
    };

    const addNewPost = (newElement) => {
        const newListPost = [...listPost];
        newListPost.push(newElement);
        setListPost(newListPost);
    }

    const uploadPost = async () => {
        try {
            const res = await UploadPost(postData);
            if (res.State === 1) {
                addNewPost(res.Data)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const getMyListPost = async () => {
        try {
            const res = await GetMyPost();
            if (res.State === 1) {
                setListPost(res.Data.listPost)
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const getListFaculty = async () => {
        try {
            const res = await GetFacultyPublic();
            if (res.State === 1) {
                setListFaculty(res.Data.listFaculty)
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    useEffect(() => {
        getMyListPost()
        getListFaculty()
    }, [])
    return (<>
        <div className="my-post-area">
            <div className="controller-my-post">
                <Button onClick={handleShow}><i class="fa-solid fa-plus"></i>&nbsp;Create a new post ?</Button>
                <Form>
                    <Form.Check
                        type="checkbox"
                        id="showApprovedOnly"
                        label="Show Approved Only"
                        checked={showApprovedOnly}
                        onChange={handleCheckboxChange}
                        style={{ background: 'blue', color: 'white', marginTop: '10%', borderRadius: '10px' }}
                    />
                </Form>
                <Modal show={show} onHide={handleClose} backdrop="static" centered>
                    <Form onSubmit={handleSubmit}>
                        <Modal.Header closeButton>
                            <Modal.Title>Upload a new post</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form.Group controlId="facultyId">
                                <Form.Label>Faculty</Form.Label>
                                <Form.Control
                                    as="select"
                                    name="facultyId"
                                    value={postData.facultyId}
                                    onChange={handleFacultyChange}
                                >
                                    <option value="">-- Select Faculty --</option>
                                    {listFaculty.map((faculty) => (
                                        <option key={faculty.id} value={faculty.id}>
                                            {faculty.name}
                                        </option>
                                    ))}
                                </Form.Control>
                            </Form.Group>
                            <Form.Group controlId="title">
                                <Form.Label>Title</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="title"
                                    value={postData.title}
                                    onChange={handleChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="avatarPost">
                                <Form.Label>Avatar Post</Form.Label>
                                <Form.Control
                                    type="file"
                                    name="avatarPost"
                                    onChange={handleChange}
                                    accept="image/*"
                                />
                                {postData.avatarPost ? (
                                    <div>
                                        <h5>Preview:</h5>
                                        <Image src={URL.createObjectURL(postData.avatarPost)} alt="Preview" thumbnail />
                                    </div>
                                ) : (
                                    <div>
                                        <h5>Preview:</h5>
                                        <Image src={DefaultImg} alt="Preview" thumbnail />
                                    </div>
                                )}
                            </Form.Group>
                            <Form.Group controlId="document">
                                <Form.Label>Document (.docx)</Form.Label>
                                <Form.Control
                                    type="file"
                                    name="document"
                                    onChange={handleChange}
                                    accept=".docx"
                                />
                            </Form.Group>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={handleClose}>
                                Close
                            </Button>
                            <Button variant="primary" type="submit" onClick={handleClose}>
                                Upload Post
                            </Button>
                        </Modal.Footer>
                    </Form>
                </Modal>
            </div>
            <div className="list-post">
                {(listPost && listPost.length > 0) ? listPost.map((item, index) => {
                    if (showApprovedOnly && !item.isApproved) {
                        return null;
                    }
                    return (<div key={item.id} className="list-post item">
                        <Image src={item.avatarPost} />
                        <div className={item.isApproved ? 'approved' : 'no-approved'}>
                            <p>{item.title}</p>
                            <i class="fa-solid fa-file-word"></i>&nbsp;&nbsp;{item.isApproved ? 'Approved' : 'Your post is not approve'}
                            <br />Upload Date: {item.dateUpload}
                        </div>
                    </div>)
                }) : <div> No Post ! </div>}
            </div>
        </div>
    </>)
}

export default Post