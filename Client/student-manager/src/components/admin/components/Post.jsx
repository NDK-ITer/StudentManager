import { useEffect, useState } from 'react';
import { Container, Card, Button, Form } from 'react-bootstrap';
import { toast } from "react-toastify"
import { GetAllPost } from '../../../api/services/PostService';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';
import { GetReportAllFaculty } from '../../../api/services/FacultyService';
import { GetAllFaculty } from '../../../api/services/FacultyService'
import { GetDateValue, GetReportFaculty } from '../../../api/services/PostService';


const Post = () => {

    const [listPost, setListPost] = useState([])
    const [dataReport, setDataReport] = useState({})
    const [dataReportFaculty, setDataReportFaculty] = useState({})
    const [listFacultyOption, setListFacultyOption] = useState()
    const [selectedFaculty, setSelectedFaculty] = useState('')
    const [fromYear, setFromYear] = useState();
    const [toYear, setToYear] = useState();
    const [selectedFromYear, setSelectedFromYear] = useState(0);
    const [selectedToYear, setSelectedToYear] = useState(0);
    const [transformedData, setTransformedData] = useState([])

    const years = [];
    for (let year = fromYear; year <= toYear; year++) {
        years.push(year);
    }

    const chartData = Object.keys(dataReport).map(key => ({
        name: key,
        postNumber: dataReport[key]
    }));

    const getDateValue = async () => {
        try {
            const res = await GetDateValue()
            if (res.State === 1) {
                setFromYear(res.Data.minYear)
                setToYear(res.Data.maxYear)
                selectedFromYear(res.Data.minYear)
                setSelectedToYear(res.Data.maxYear)
            } else {
                toast.warning(res.data.mess)
            }
        } catch (error) {
            toast.error(error);
        }
    }

    const handleFromYearChange = (event) => {
        const selectedYear = parseInt(event.target.value);
        setSelectedFromYear(selectedYear);
    };

    const handleToYearChange = (event) => {
        const selectedYear = parseInt(event.target.value);
        setSelectedToYear(selectedYear);
    };

    const handleSelectChange = (event) => {
        setSelectedFaculty(event.target.value);
    };

    const getAllFaculty = async () => {
        try {
            const res = await GetAllFaculty()
            if (res.State === 1) {
                setListFacultyOption(res.Data.listFaculty)
            } else {
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

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

    const handleGetData = () => {
        getDataReport(selectedFaculty, selectedFromYear, selectedToYear)
    };

    const getDataReport = async (facultyId, fromYear, toYear) => {
        try {
            const res = await GetReportFaculty(facultyId, fromYear, toYear)
            if (res.State === 1 && res.Data !== null) {
                setDataReportFaculty(res.Data)
                // if (dataReportFaculty.approved === null && dataReportFaculty.nonApproved === null) {
                //     console.log(1)
                //     return
                // } else if (dataReportFaculty.approved == null && !dataReportFaculty.nonApproved == null) {
                //     setTransformedData(Object.keys(dataReportFaculty.nonApproved).map(year => ({
                //         name: year,
                //         approved: dataReportFaculty.approved? dataReportFaculty.nonApproved[year] : 0,
                //         nonApproved: dataReportFaculty.nonApproved[year]
                //     })))
                //     toast.warning(1)

                // } else if (dataReportFaculty.nonApproved == null && !dataReportFaculty.approved == null) {
                //     setTransformedData(Object.keys(dataReportFaculty.approved).map(year => ({
                //         name: year,
                //         approved: dataReportFaculty.approved[year],
                //         nonApproved: dataReportFaculty.nonApproved ? dataReportFaculty.nonApproved[year] : 0 // Xử lý giá trị null
                //     })))
                // }
                setTransformedData(Object.keys(dataReportFaculty.approved).map(year => ({
                    name: year,
                    approved: dataReportFaculty.approved[year],
                    nonApproved: dataReportFaculty.nonApproved ? dataReportFaculty.nonApproved[year] : 0 // Xử lý giá trị null
                })))
            } else {
                toast.warning(res.Data.mess)
            }
        } catch (error) {
            toast.error(error);
        }
    }

    useEffect(() => {
        getAllPost()
        getDateReport()
        getAllFaculty()
        getDateValue()
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
            <div>
                <Form.Group controlId="exampleForm.SelectCustom">
                    <Form.Select value={selectedFaculty} onChange={handleSelectChange}>
                        <option value="">---Select a faculty---</option>
                        {listFacultyOption && listFacultyOption.map((option, index) => (
                            <option key={index} value={option.id}>{option.name}</option>
                        ))}
                    </Form.Select>
                </Form.Group>
                <Form.Group controlId="exampleForm.SelectCustom">
                    <Form.Label>From Year:</Form.Label>
                    <Form.Control as="select" custom onChange={handleFromYearChange} value={selectedFromYear}>
                        {years.map((year) => (
                            <option key={year} value={year}>
                                {year}
                            </option>
                        ))}
                    </Form.Control>
                </Form.Group>
                <Form.Group controlId="exampleForm.SelectCustom">
                    <Form.Label>To Year:</Form.Label>
                    <Form.Control as="select" custom onChange={handleToYearChange} value={selectedToYear}>
                        {years.map((year) => (
                            <option key={year} value={year}>
                                {year}
                            </option>
                        ))}
                    </Form.Control>
                </Form.Group>
                <Button variant="primary" onClick={handleGetData}>Get Data</Button>
                <BarChart
                    width={1300}
                    height={800}
                    data={transformedData}
                    margin={{
                        top: 5, right: 30, left: 20, bottom: 5,
                    }}
                >
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="name" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Bar dataKey="approved" fill="#8884d8" />
                    <Bar dataKey="nonApproved" fill="#010101" />
                </BarChart>
            </div>
        </Container>
    </>)
}

export default Post