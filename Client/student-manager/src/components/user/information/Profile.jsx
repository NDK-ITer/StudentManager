import { Card, Row, Col } from 'react-bootstrap';


const Profile = (props) => {
    return (<>
        <div>
            <Row className="justify-content-md-center">
                <Col md={13}>
                    <Card>
                        <Card.Body>
                            <div className="d-flex justify-content-between align-items-center">
                                <div className='props'>
                                    <Card.Title><i class="fa-solid fa-signature"></i></Card.Title>
                                    <Card.Text>
                                        <p>{props.data.fullName}</p>
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
                        <Card.Body>
                            <div className="d-flex justify-content-between align-items-center">
                                <div className='props'>
                                    <Card.Title><i class="fa-solid fa-signature"></i></Card.Title>
                                    <Card.Text>
                                        <p>{props.data.fullName}</p>
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