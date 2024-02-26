import React from 'react';
import Offcanvas from 'react-bootstrap/Offcanvas';

const Sidebar = ({ show, handleClose }) => {
    return (<>
        <div>
            <Offcanvas show={show} onHide={handleClose} placement="bottom">
                <Offcanvas.Header >
                </Offcanvas.Header>
                <Offcanvas.Body>
                    <div>
                        <div className="option-list">
                            <div className="option-list-item">
                                <i class="bi bi-people-fill"></i> User
                            </div>
                            <div className="option-list-item">
                                <i class="bi bi-file-earmark-post"></i> Post
                            </div>
                            <div className="option-list-item">
                            <i class="fa-solid fa-people-roof"></i> Faculty
                            </div>
                        </div>
                    </div>
                </Offcanvas.Body>
            </Offcanvas>
        </div>
    </>);
};

export default Sidebar;
