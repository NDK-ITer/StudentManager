import React from 'react';
import Offcanvas from 'react-bootstrap/Offcanvas';
import { Link } from 'react-router-dom';

const Sidebar = ({ show, handleClose }) => {
    const baseUrl = '/admin'
    return (<>
        <div>
            <Offcanvas show={show} onHide={handleClose} placement="bottom">
                <Offcanvas.Header >
                </Offcanvas.Header>
                <Offcanvas.Body>
                    <div>
                        <div className="option-list">
                            <div className="option-list-item">
                                <Link className='link-style' to={`${baseUrl}/user`}>
                                    <i class="bi bi-people-fill"></i>
                                    <br />User
                                </Link>
                            </div>
                            <div className="option-list-item">
                                <Link className='link-style' to={`${baseUrl}/faculty`}>
                                    <i class="fa-solid fa-people-roof"></i>
                                    <br />Faculty
                                </Link>
                            </div>
                            <div className="option-list-item">
                                <Link className='link-style' to={`${baseUrl}/post`}>
                                    <i class="bi bi-file-earmark-post"></i>
                                    <br />Post
                                </Link>
                            </div>
                        </div>
                    </div>
                </Offcanvas.Body>
            </Offcanvas>
        </div>
    </>);
};

export default Sidebar;
