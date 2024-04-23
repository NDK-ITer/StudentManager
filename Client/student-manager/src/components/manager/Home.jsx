import React, { useState, useEffect, useContext } from 'react';
import ReactApexChart from 'react-apexcharts';
import { UserContext } from '../../contexts/UserContext'
import { toast } from "react-toastify"
import { GetReportFaculty } from '../../api/services/PostService';
import { GetDateValue } from '../../api/services/PostService';
import { Form, Button } from 'react-bootstrap';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';


const Home = () => {
    const { user } = useContext(UserContext)
    const [dataReport, setDataReport] = useState({})
    const [fromYear, setFromYear] = useState();
    const [toYear, setToYear] = useState();
    const [selectedFromYear, setSelectedFromYear] = useState(0);
    const [selectedToYear, setSelectedToYear] = useState(0);
    const [transformedData, setTransformedData] = useState([])

    const chartData = Object.keys(dataReport).map(key => ({
        name: key,
        postNumber: dataReport[key]
    }));

    const handleFromYearChange = (event) => {
        const selectedYear = parseInt(event.target.value);
        setSelectedFromYear(selectedYear);
    };

    const handleToYearChange = (event) => {
        const selectedYear = parseInt(event.target.value);
        setSelectedToYear(selectedYear);
    };

    const years = [];
    for (let year = fromYear; year <= toYear; year++) {
        years.push(year);
    }


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

    const getDataReport = async (fromYear, toYear) => {
        try {
            const res = await GetReportFaculty(user.facultyId, fromYear, toYear)
            if (res.State === 1) {
                setDataReport(res.Data)
                setTransformedData(Object.keys(dataReport.approved).map(year => ({
                    name: year,
                    approved: dataReport.approved[year],
                    nonApproved: dataReport.nonApproved ? dataReport.nonApproved[year] : 0 // Xử lý giá trị null
                })))
            } else {
                toast.warning(res.data.mess)
            }
        } catch (error) {
            toast.error(error);
        }
    }

    const handleGetData = () => {
        getDataReport(selectedFromYear, selectedToYear)
    };


    const state = {
        options: {
            chart: {
                id: 'basic-bar'
            },
            xaxis: {
                categories: Object.keys(dataReport)
            }
        },
        series: [
            {
                name: 'NDK',
                data: Object.values(dataReport)
            }
        ]
    };
    useEffect(() => {
        getDateValue().then(getDataReport(fromYear, toYear))
        setToYear(selectedFromYear);
    }, []);
    return (
        <div className='manager-home-content'>
            <div className='manager-chart'>
                {/* <ReactApexChart options={state.options} series={state.series} type="bar" height={620} width={900} /> */}
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
            <div className='manager-chart-controller' style={{ color: 'black' }}>
                <div>
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
                </div>
            </div>
        </div>
    );
};

export default Home;
