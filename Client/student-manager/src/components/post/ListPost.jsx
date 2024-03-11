import DateController from "./DateController"
import FacultyController from "./FacultyController"
import { GetPostPublic } from '../../api/services/PostService'
import { GetFacultyPublic } from '../../api/services/FacultyService'
import { toast } from "react-toastify"
import { useEffect, useState } from "react"
import { Button, Form, Card, FormControl } from "react-bootstrap"
import { Link } from "react-router-dom"

const ListPost = () => {
    const [listPost, setListPost] = useState([])
    const [searchTerm, setSearchTerm] = useState('');
    const [listFaculty, setListFaculty] = useState([])
    const [selectedFaculty, setSelectedFaculty] = useState([]);

    const handleCheckboxChange = async (id) => {
        await setSelectedFaculty(prevSelectedFaculty => {
            const selectedIndex = prevSelectedFaculty.indexOf(id);
            let updatedItems = [...prevSelectedFaculty];

            if (selectedIndex === -1) {
                updatedItems.push(id);
            } else {
                updatedItems.splice(selectedIndex, 1);
            }
            return updatedItems;
        });

    };

    const getFacultyPublic = async () => {
        try {
            const res = await GetFacultyPublic();
            if (res.State === 1) {
                setListFaculty(res.Data.listFaculty)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const handleSearchChange = (event) => {
        setSearchTerm(event.target.value);
    };

    const filteredPosts = listPost.filter(item => {
        const titleFilter = item.title.toLowerCase().includes(searchTerm.toLowerCase())
        const facultyFilter = selectedFaculty.includes(item.facultyId);
        return titleFilter || facultyFilter
    });

    const getPostPublic = async () => {
        try {
            const res = await GetPostPublic()
            if (res.State === 1) {
                setListPost(res.Data.listPost)
            }
            else {
                toast.warning(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    useEffect(() => {
        getPostPublic()
        getFacultyPublic()

    }, [])

    return (<>
        <div className="list-post-content">
            <div className="faculty-controller">
                <div className="search-post">
                    <Form inline>
                        <FormControl
                            type="text"
                            placeholder="Search"
                            className="mr-sm-2"
                            value={searchTerm}
                            onChange={handleSearchChange}
                        />
                    </Form>
                </div>
                {listFaculty && (<FacultyController listFaculty={listFaculty} selectedFaculty={selectedFaculty} handleCheckboxChange={handleCheckboxChange} />)}
            </div>
            <div className="list-post-element">
                {listPost && listPost.length > 0 && filteredPosts.map((item, index) => {
                    return (<div className="element-post-public">
                        <Card style={{ width: '18rem' }}>
                            <Card.Img variant="top" src={item.avatarPost} style={{ height: '170px', objectFit: 'cover' }} />
                            <Card.Header style={{ color: 'blue' }}>Posted at: {item.uploadDate}</Card.Header>
                            <Card.Body>
                                <Card.Title>{item.title}</Card.Title>
                                <Card.Text>
                                    {item.description}
                                </Card.Text>
                                <Link to={`/post/${item.id}`}>
                                    <Button variant="primary"><i class="bi bi-arrow-right-square"></i>&nbsp;See More Information</Button>
                                </Link>
                            </Card.Body>
                        </Card>
                    </div>)
                })}
            </div>
            <div className="date-post-controller">
                <DateController />
                {selectedFaculty && selectedFaculty.map((item, index) =>{
                    return(<div>
                        {item}
                    </div>)
                })}
            </div>
        </div>
    </>)
}

export default ListPost