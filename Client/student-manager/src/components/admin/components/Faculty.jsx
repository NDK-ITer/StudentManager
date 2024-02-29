import HomeAdmin from '../../../assets/images/home-admin.png'
import { Card, Button, Form, FloatingLabel  } from 'react-bootstrap';
import FacultyLogoItem from '../../../assets/images/faculty-logo-item.png'
const Faculty = () => {

    return (<>
        <div className="faculty-main image-container" style={{ backgroundImage: `url(${HomeAdmin})` }}>
            <div className="faculty-crud">
                <div className='faculty-crud-form'>
                    <h2>Create Or Update</h2>
                    <Form>
                        <FloatingLabel
                            controlId="floatingInput"
                            label="Name"
                            className="mb-3"
                        >
                            <Form.Control type="text" placeholder=""/>
                        </FloatingLabel>
                        <Button variant="success">Create</Button>{' '}
                    </Form>
                </div>
            </div>
            <div className="faculty-content">
                <div className="faculty-search-box">
                    <Form >
                        <Form.Control
                            type="text"
                            placeholder="Search"
                            className=" mr-sm-2"
                        />
                    </Form>
                </div>
                <div className="faculty-list">
                    <div className='faculty-list-item'>
                        <Card style={{ width: '18rem', opacity: '1', boxShadow: '0px 0px 10px rgba(0, 0, 0, 1)' }}>
                            <Card.Img variant="top" src={FacultyLogoItem} />
                            <Card.Body>
                                <Card.Title>Card Title</Card.Title>
                            </Card.Body>
                        </Card>
                    </div>

                    <div className='faculty-list-item'>
                        <Card style={{ width: '18rem', opacity: '1', boxShadow: '0px 0px 10px rgba(0, 0, 0, 1)' }}>
                            <Card.Img variant="top" src={FacultyLogoItem} />
                            <Card.Body>
                                <Card.Title>Card Title</Card.Title>
                            </Card.Body>
                        </Card>
                    </div>

                    <div className='faculty-list-item'>
                        <Card style={{ width: '18rem', opacity: '1', boxShadow: '0px 0px 10px rgba(0, 0, 0, 1)' }}>
                            <Card.Img variant="top" src={FacultyLogoItem} />
                            <Card.Body>
                                <Card.Title>Card Title</Card.Title>
                            </Card.Body>
                        </Card>
                    </div>

                    <div className='faculty-list-item'>
                        <Card style={{ width: '18rem', opacity: '1', boxShadow: '0px 0px 10px rgba(0, 0, 0, 1)' }}>
                            <Card.Img variant="top" src={FacultyLogoItem} />
                            <Card.Body>
                                <Card.Title>Card Title</Card.Title>
                            </Card.Body>
                        </Card>
                    </div>

                </div>
            </div>
        </div>
    </>)
}

export default Faculty