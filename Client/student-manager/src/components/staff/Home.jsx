import { useEffect, useState } from "react";
import { toast } from "react-toastify"
import { GetListUser, SetStudent } from "../../api/services/UserService";
import { Card, Button } from 'react-bootstrap';
import { Container } from 'react-bootstrap';
function Home() {

    const [listUser, setListUser] = useState([])

    const getListUser = async () => {
        try {
            const res = await GetListUser()
            if (res.State === 1) {
                setListUser(res.Data.listUser)
            }
            else {
                toast.warning(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const deleteUser = (idToDelete) => {
        const updatedList = [...listUser];
        const indexToDelete = updatedList.findIndex(user => user.id === idToDelete);
        if (indexToDelete !== -1) {
            updatedList.splice(indexToDelete, 1);
            setListUser(updatedList);
        }
    };

    const updateToStudent = async (id) => {
        try {
            const res = await SetStudent(id)
            if (res.State === 1) {
                deleteUser(res.Data.id)
                toast.success('Update successful!')
            }
        } catch (error) {
            toast.error(error)
        }
    }

    useEffect(() => {
        getListUser()
    }, [])
    return (<>

        <Container>
            <div style={{
                display: "flex",
                flexWrap: 'wrap',
                gap: '20px',
                justifyContent: 'space-between',
                margin: "auto",
                marginTop: "5%",
            }}>
                {listUser && listUser.length > 0 ? listUser.map((item, index) => {
                    return (<>
                        <Card style={{ width: '18rem' }}>
                            <div style={{ maxHeight: '200px', overflow: 'hidden' }}>
                                <Card.Img variant="top" src={item.avatar} style={{ width: '100%', height: 'auto' }} />
                            </div>
                            <Card.Body>
                                <Card.Title>{item.fullName}</Card.Title>
                                <Card.Text>
                                    {item.email}<br />
                                    {item.userName}
                                </Card.Text>
                                <Button variant="primary" onClick={() => updateToStudent(item.id)} >Update student</Button>
                            </Card.Body>
                        </Card>
                    </>)
                }) : <div style={{
                    margin: 'auto',
                }}>We don't have User in here</div>}

            </div>
        </Container>

    </>)
}

export default Home