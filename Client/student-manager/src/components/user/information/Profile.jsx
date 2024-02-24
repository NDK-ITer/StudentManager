import { Card, Container, Row, Col } from 'react-bootstrap';


const Profile = (props) => {
    return (<>
        <div>
            <Row className="justify-content-md-center">
                <Col md={8}>
                    <Card>
                        <Card.Body>
                            <Card.Title>Email</Card.Title>
                            <Card.Text>
                                <p>Email: {props.data.email}</p>
                            </Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </div>
    </>)
}

export default Profile