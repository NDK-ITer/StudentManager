import HomeAdmin from '../../../assets/images/home-admin.png'
import { Card, Button, Form, FloatingLabel, Modal, Pagination, } from 'react-bootstrap';
import FacultyLogoItem from '../../../assets/images/faculty-logo-item.png'
import { useEffect, useState } from 'react';
import { CreateFaculty, DeleteFaculty, UpdateFaculty, GetAllFaculty, RestoreFaculty } from '../../../api/services/FacultyService';
import { toast } from 'react-toastify';

const Faculty = () => {

    const [listFaculty, setListFaculty] = useState([])
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage] = useState(3);
    const [searchTerm, setSearchTerm] = useState('');
    const [facultyData, setFacultyData] = useState({
        name: ''
    })
    const [isOpenFacultyDetail, setIsFacultyDetail] = useState(Array(listFaculty.length).fill(false))
    const filteredListFaculty = listFaculty.filter(faculty => {
        const searchTermLower = searchTerm.toLowerCase();
        const nameFacultyMatch = faculty.name.toLowerCase().includes(searchTermLower);

        return nameFacultyMatch;
    }
    );
    const startIndex = (currentPage - 1) * itemsPerPage;
    const endIndex = Math.min(startIndex + itemsPerPage - 1, listFaculty.length - 1);

    // Danh sách faculty trên trang hiện tại
    let currentFaculty = filteredListFaculty.slice(startIndex, endIndex + 1);

    // Nếu trang cuối không đủ 3 phần tử, thêm phần tử trống vào
    while (currentFaculty.length < itemsPerPage) {
        currentFaculty.push({ name: '', isDelete: false }); // Thêm phần tử giả mạo với dữ liệu trống
    }

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    const addNewFaculty = (newElement) => {
        const newListFaculty = [...listFaculty];
        newListFaculty.push(newElement);
        setListFaculty(newListFaculty);
    };

    const updateFacultyFields = (facultyId, newFields) => {
        setListFaculty(prevList => {
            return prevList.map(faculty => {
                if (faculty.id === facultyId) {
                    return {
                        ...faculty,
                        ...newFields
                    };
                }
                return faculty;
            });
        });
    };

    const createFaculty = async (event) => {
        event.preventDefault();
        try {
            const res = await CreateFaculty({ name: facultyData.name });
            if (res.State === 1) {
                toast.success("Create successful!");
                addNewFaculty(res.Data);
            } else {
                toast.error(res.Data.mess);
            }
        } catch (error) {
            toast.error(error.message);
        }
    }

    const updateFaculty = async () => {
        try {
            const res = await UpdateFaculty({ id: facultyData.id, name: facultyData.name })
            if (res.State === 1) {
                toast.success("Update successful!")
                updateFacultyFields(res.Data.id, { name: res.Data.name })
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const deleteFaculty = async (id) => {
        try {
            const res = await DeleteFaculty(id)
            if (res.State === 1) {
                toast.success(res.Data.mess)
                updateFacultyFields(res.Data.id, { isDelete: res.Data.isDelete })
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const restoreFaculty = async (id) => {
        try {
            const res = await RestoreFaculty(id)
            if (res.State === 1) {
                toast.success(res.Data.mess)
                updateFacultyFields(res.Data.id, { isDelete: res.Data.isDelete })
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const togglePopup = (index) => {
        const newIsOpenFacultyDetail = [...isOpenFacultyDetail];
        newIsOpenFacultyDetail[index] = !newIsOpenFacultyDetail[index];
        setIsFacultyDetail(newIsOpenFacultyDetail);
    };

    const handleSearchChange = (event) => {
        setSearchTerm(event.target.value);
    };



    const getAllFaculty = async () => {
        try {
            const res = await GetAllFaculty()
            if (res.State === 1) {
                setListFaculty(res.Data.listFaculty)
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }



    useEffect(() => {
        getAllFaculty()
    }, [])
    return (<>
        <div className="faculty-main image-container" style={{ backgroundImage: `url(${HomeAdmin})` }}>
            <div className="faculty-crud">
                <div className='faculty-crud-form'>
                    <h2>Create new Faculty</h2>
                    <Form onSubmit={createFaculty}>
                        <FloatingLabel
                            controlId="floatingInput"
                            label="Name"
                            className="mb-3"
                            onChange={(event) =>
                                setFacultyData({
                                    ...facultyData,
                                    name: event.target.value
                                })
                            }
                        >
                            <Form.Control type="text" placeholder="" />
                        </FloatingLabel>
                        <Button
                            type="submit"
                            variant="success"
                            disabled={facultyData.name ? false : true}
                        >
                            Create
                        </Button>
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
                            onChange={handleSearchChange}
                        />
                    </Form>
                </div>
                <div className="faculty-list">
                    {currentFaculty.map((item, index) => (
                        <div key={index} className='faculty-list-item' onClick={() => {
                            setFacultyData({
                                id: item.id,
                                name: item.name
                            })
                            togglePopup(index)
                        }}>
                            {item.name && ( // Kiểm tra item.name có tồn tại không trước khi truy cập
                                <Card style={{ width: '18rem', opacity: '1', boxShadow: '0px 0px 10px rgba(0, 0, 0, 1)' }}>
                                    <Card.Img variant="top" src={FacultyLogoItem} />
                                    <Card.Body>
                                        <Card.Title>{item.name}</Card.Title>
                                    </Card.Body>
                                    <Card.Footer className={item.isDelete ? 'delete' : 'non-delete'} style={{ height: '50px' }}>
                                        <div style={{ display: 'auto', color: 'white', textAlign: 'center', fontSize: 'large' }}>{item.isDelete ? 'Deleted' : ''}</div>
                                    </Card.Footer>
                                </Card>
                            )}
                        </div>
                    ))}

                    {/* Phân trang */}
                    <div className="d-flex justify-content-center mt-3">
                        <Pagination>
                            {[...Array(Math.ceil(listFaculty.length / itemsPerPage)).keys()].map(pageNumber => (
                                <Pagination.Item key={pageNumber + 1} active={pageNumber + 1 === currentPage} onClick={() => handlePageChange(pageNumber + 1)}>
                                    {pageNumber + 1}
                                </Pagination.Item>
                            ))}
                        </Pagination>
                    </div>

                    {/* Modal */}
                    {currentFaculty.map((item, index) => (
                        <Modal key={index} show={isOpenFacultyDetail[index]} onHide={() => togglePopup(index)} centered>
                            <Modal.Header style={{ display: 'flex', justifyContent: 'center' }}>
                                <Modal.Title>{item.name}</Modal.Title>
                            </Modal.Header>
                            <Modal.Body>
                                <Form>
                                    <FloatingLabel
                                        controlId="floatingInput"
                                        label="New Name"
                                        className="mb-3"
                                        onChange={(event) =>
                                            setFacultyData({
                                                ...facultyData,
                                                name: event.target.value
                                            })
                                        }
                                    >
                                        <Form.Control type="text" placeholder="" value={facultyData.name} />
                                    </FloatingLabel>
                                </Form>
                            </Modal.Body>
                            <Modal.Footer style={{ display: 'flex', justifyContent: 'space-between' }}>
                                <div>
                                    {item.isDelete ?
                                        <Button variant="success" onClick={() => {
                                            togglePopup(index)
                                            restoreFaculty(item.id)
                                        }}>Restore</Button>
                                        : <Button variant="danger" onClick={() => {
                                            togglePopup(index)
                                            deleteFaculty(item.id)
                                        }}>Delete</Button>
                                    }
                                </div>
                                <div>
                                    <Button variant="primary" onClick={() => {
                                        updateFaculty()
                                        togglePopup(index)
                                    }}>
                                        Update
                                    </Button>
                                </div>
                            </Modal.Footer>
                        </Modal>
                    ))}
                </div>
            </div>
        </div>
    </>)
}

export default Faculty