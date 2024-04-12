import { Container, Row, Col, Card, } from 'react-bootstrap';
import React from 'react';
import { Image } from 'react-bootstrap';
import StudentLogo from '../../assets/images/student-logo.png'
import PostLogo from '../../assets/images/post-admin-logo.png'
import Faculty from '../../assets/images/faculty-admin-logo.png'
import HomeAdmin from '../../assets/images/home-admin.png'
import { Link } from 'react-router-dom';

const Home = () => {
    const baseUrl = '/admin'
    return (<>
        <div className="main-content image-container" style={{backgroundImage: `url(${HomeAdmin})`}}>
            <Container>
                <h1 style={{color: 'white'}}>Welcome to Greenwich</h1>
                <div className="d-flex justify-content-center">
                    <Row>
                        <Col md={6} lg={4}>
                            <Link className='link-style' to={`${baseUrl}/user`}>
                                <Card className='card-item-custom'>
                                    <Card.Body>
                                        <Card.Title>
                                            <i class="bi bi-people-fill"></i>   User
                                        </Card.Title>
                                        <Card.Text>Greenwich family</Card.Text>
                                    </Card.Body>
                                    <Image src={StudentLogo} />
                                </Card>
                            </Link>
                        </Col>
                        <Col md={6} lg={4}>
                            <Link className='link-style' to={`${baseUrl}/faculty`}>
                                <Card className='card-item-custom'>
                                    <Card.Body>
                                        <Card.Title>
                                            <i class="fa-solid fa-people-roof"></i>   Faculty
                                        </Card.Title>
                                        <Card.Text>Talent will come with you</Card.Text>
                                    </Card.Body>
                                    <Image src={Faculty} />
                                </Card>
                            </Link>
                        </Col>
                        <Col md={6} lg={4}>
                            <Link className='link-style' to={`${baseUrl}/all-post`}>
                                <Card className='card-item-custom'>
                                    <Card.Body>
                                        <Card.Title>
                                            <i class="bi bi-file-earmark-post"></i>   Post
                                        </Card.Title>
                                        <Card.Text>Can you share your opinion ?</Card.Text>
                                    </Card.Body>
                                    <Image src={PostLogo} />
                                </Card>
                            </Link>
                        </Col>
                    </Row>
                </div>
            </Container>
        </div>
    </>)
}

export default Home
