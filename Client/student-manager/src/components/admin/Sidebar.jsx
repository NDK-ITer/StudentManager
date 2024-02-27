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
                                <i class="bi bi-people-fill"></i>
                                <br />User
                            </div>
                            <div className="option-list-item">
                                <i class="fa-solid fa-people-roof"></i>
                                <br />Faculty
                            </div>
                            <div className="option-list-item">
                                <i class="bi bi-file-earmark-post"></i>
                                <br />Post
                            </div>
                        </div>
                    </div>
                </Offcanvas.Body>
            </Offcanvas>
        </div>
    </>);
};

export default Sidebar;
