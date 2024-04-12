import React, { useState, useEffect, useContext } from 'react';
import ReactApexChart from 'react-apexcharts';
import { UserContext } from '../../contexts/UserContext'
import { toast } from "react-toastify"
import { GetReportFaculty } from '../../api/services/PostService';
import { GetDateValue } from '../../api/services/PostService';
import { Form, Button } from 'react-bootstrap';

const Home = () => {
    const { user } = useContext(UserContext)
    const [dataReport, setDataReport] = useState({})
    const [fromYear, setFromYear] = useState();
    const [toYear, setToYear] = useState();
    const [selectedFromYear, setSelectedFromYear] = useState(0);
    const [selectedToYear, setSelectedToYear] = useState(0);

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
                <ReactApexChart options={state.options} series={state.series} type="line" height={620} width={900} />
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
