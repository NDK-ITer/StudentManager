import { Form, Button, Modal, Image, Container, Row, Col } from 'react-bootstrap';
import 'react-image-crop/dist/ReactCrop.css';
import DefaultImg from '../../../assets/images/default-img.png'
import { useState } from 'react';
import { toast } from 'react-toastify';
import { UploadAvatar } from '../../../api/services/AuthService';


const EditAvatar = ({ show, handleClose }) => {

    const [image, setImage] = useState(null);
    const [loadingApi, setLoadingApi] = useState(false)

    const handleImageChange = (e) => {
        const selectedImage = e.target.files[0];
        setImage(selectedImage);
    };

    const handleSubmitUploadImage = async(event) => {
        event.preventDefault();
        try {
            if (!image) {
                toast.error("Please choose your picture")
            }
            setLoadingApi(true)
            const res = await UploadAvatar({
                uploadAvatar:image
            });
            if (res.State === 1) {
                toast.success(res.Data.mess)
                handleClose()
            }else{
                toast.error(res.Data.mess)
            }
            setLoadingApi(false)
        } catch (error) {
            toast.error(error)
            setLoadingApi(false)
        }
        setLoadingApi(false)
    }

    return (<>
        <Modal show={show} onHide={handleClose} centered>
            <Modal.Header closeButton>
                <Modal.Title>Upload your avatar</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Container>
                    <Row>
                        <Col xs={12}>
                            <Form.Group controlId="formFile" className="mb-3">
                                <Form.Label>Select an image</Form.Label>
                                <Form.Control type="file" accept="image/*" onChange={handleImageChange} />
                            </Form.Group>
                            {image ? (
                                <div>
                                    <h5>Preview:</h5>
                                    <Image src={URL.createObjectURL(image)} alt="Preview" thumbnail />
                                </div>
                            ) : (
                                <div>
                                    <h5>Preview:</h5>
                                    <Image src={DefaultImg} alt="Preview" thumbnail />
                                </div>
                            )}
                        </Col>
                    </Row>
                </Container>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="primary" onClick={handleSubmitUploadImage} disabled={image ? false:true} >
                    {loadingApi && (<i class="fa-solid fa-camera-rotate fa-flip"></i>)} Save Changes
                </Button>
            </Modal.Footer>
        </Modal>
    </>)
}

export default EditAvatar