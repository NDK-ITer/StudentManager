import { Form, Button, Modal, Image, Container, Row, Col } from 'react-bootstrap';
import ReactCrop from 'react-image-crop';
import 'react-image-crop/dist/ReactCrop.css';
import DefaultImg from '../../../assets/images/default-img.png'
import { useState } from 'react';


const CutImage = ({ show, handleClose }) => {

    const [image, setImage] = useState(null);
    const [crop, setCrop] = useState({ aspect: 1 / 1 });
    const [croppedImage, setCroppedImage] = useState(null);

    const handleImageChange = (e) => {
        const selectedImage = e.target.files[0];
        setImage(selectedImage);
    };

    const handleCropChange = (crop) => {
        setCrop(crop);
    };

    const handleCropComplete = (crop, pixelCrop) => {
        if (image && pixelCrop.width && pixelCrop.height) {
            const canvas = document.createElement('canvas');
            canvas.width = pixelCrop.width;
            canvas.height = pixelCrop.height;
            const ctx = canvas.getContext('2d');
    
            const imageToCrop = new Image();
            imageToCrop.src = URL.createObjectURL(image);
    
            // Kiểm tra xem ảnh đã hoàn toàn tải chưa
            if (imageToCrop.complete) {
                // Nếu ảnh đã hoàn toàn tải, vẽ nó lên canvas ngay lập tức
                ctx.drawImage(
                    imageToCrop,
                    pixelCrop.x,
                    pixelCrop.y,
                    pixelCrop.width,
                    pixelCrop.height,
                    0,
                    0,
                    pixelCrop.width,
                    pixelCrop.height
                );
    
                const base64Image = canvas.toDataURL('image/jpeg');
                setCroppedImage(base64Image);
            } else {
                // Nếu ảnh chưa hoàn toàn tải, chờ đến khi nó tải xong trước khi vẽ lên canvas
                imageToCrop.onload = () => {
                    ctx.drawImage(
                        imageToCrop,
                        pixelCrop.x,
                        pixelCrop.y,
                        pixelCrop.width,
                        pixelCrop.height,
                        0,
                        0,
                        pixelCrop.width,
                        pixelCrop.height
                    );
    
                    const base64Image = canvas.toDataURL('image/jpeg');
                    setCroppedImage(base64Image);
                };
            }
        }
    };
    

    return (<>
        <Modal show={show} onHide={handleClose} centered>
            <Modal.Header closeButton>
                <Modal.Title>Upload your avatar</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Container>
                    <Row>
                        <Col xs={12} md={6}>
                            <Form.Group controlId="formFile" className="mb-3">
                                <Form.Label>Select an image</Form.Label>
                                <Form.Control type="file" onChange={handleImageChange} />
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
                        <Col xs={12} md={6}>
                            <h5>Crop Image</h5>
                            {image && (
                                <ReactCrop
                                    src={URL.createObjectURL(image)}
                                    crop={crop}
                                    onChange={handleCropChange}
                                    onComplete={handleCropComplete}
                                />
                            )}
                            {croppedImage && (
                                <div>
                                    <h5>Cropped Image:</h5>
                                    <Image src={croppedImage} alt="Cropped Image" thumbnail />
                                </div>
                            )}
                        </Col>
                    </Row>
                </Container>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="primary" onClick={handleClose}>
                    Save Changes
                </Button>
            </Modal.Footer>
        </Modal>
    </>)
}

export default CutImage