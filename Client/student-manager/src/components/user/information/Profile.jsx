import { useState } from 'react';
import { Card, Row, Col, Button } from 'react-bootstrap';
import EditProfile from './EditProfile';


const Profile = (props) => {
    const [isEditProfileShow, setIsEditProfileShow] = useState(false)

    const handleEditProfileClose = () => {
        setIsEditProfileShow(false)
    }

    const handleEditProfileOpen = () => {
        setIsEditProfileShow(true)
    }
    return (<>
        <div>
            <Row className="justify-content-md-center">
                <Col md={13}>
                    <Card>
                        <Card.Header className="text-center" style={{ backgroundColor: 'grey', color: 'white' }}>
                            <div onClick={handleEditProfileOpen}>
                                <Button className='btn-edit-user' style={{ borderRadius: '100px' }}>
                                    <i class="fa-solid fa-user-pen"></i>
                                </Button>
                            </div>
                            <EditProfile show={isEditProfileShow} onClose={handleEditProfileClose}
                                data={{
                                    firstName: props.data.firstName,
                                    lastName: props.data.lastName,
                                    userName: props.data.userName
                                }}
                            />
                        </Card.Header>
                        <Card.Body>
                            <div className="d-flex justify-content-between align-items-center">
                                <div className='props'>
                                    <Card.Title><i class="fa-solid fa-signature"></i></Card.Title>
                                    <Card.Text>
                                        <p>{`${props.data.firstName} ${props.data.lastName}`}</p>
                                    </Card.Text>
                                </div>
                                <div className='props'>
                                    <Card.Title><i class="fa-solid fa-envelope"></i></Card.Title>
                                    <Card.Text>
                                        <p>{props.data.email}</p>
                                    </Card.Text>
                                </div>
                            </div>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </div>
    </>)
}

export default Profile