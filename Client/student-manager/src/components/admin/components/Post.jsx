import { useEffect, useState } from 'react';
import { Container, Card, Button } from 'react-bootstrap';
import { toast } from "react-toastify"
import { GetAllPost } from '../../../api/services/PostService';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';
import { GetReportAllFaculty } from '../../../api/services/FacultyService';

const Post = () => {

    const [listPost, setListPost] = useState([])
    const [dataReport, setDataReport] = useState({})

    const chartData = Object.keys(dataReport).map(key => ({
        name: key,
        postNumber: dataReport[key]
    }));

    const getAllPost = async () => {
        try {
            const res = await GetAllPost()
            if (res.State === 1) {
                setListPost(res.Data.listPost)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const getDateReport = async () => {
        try {
            const res = await GetReportAllFaculty()
            if (res.State === 1) {
                setDataReport(res.Data)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    useEffect(() => {
        getAllPost()
        getDateReport()
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
                height: "100vh",
                overflowY: "auto",
            }}>
                {listPost && listPost.length > 0 && listPost.map((item, index) => {
                    return (<>
                        <Card style={{ width: '18rem' }}>
                            <Card.Img variant="top" src={item.avatarPost} />
                            <Card.Body>
                                <Card.Title>{item.title}</Card.Title>
                                <Card.Text>
                                    Post at: {item.uploadDate}
                                </Card.Text>
                                <Button variant="primary"><a href={item.linkDocPost} style={{ textDecoration: 'none', color: 'inherit' }}>Download Post</a></Button>
                            </Card.Body>
                        </Card>
                    </>)
                })}
            </div>
            <div>
                <BarChart
                    width={1300}
                    height={800}
                    data={chartData}
                    margin={{
                        top: 5, right: 30, left: 20, bottom: 5,
                    }}
                >
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="name" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Bar dataKey="postNumber" fill="#8884d8" />
                </BarChart>
            </div>
        </Container>
    </>)
}

export default Post