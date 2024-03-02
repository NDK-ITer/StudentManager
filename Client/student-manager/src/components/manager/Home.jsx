import React, { useState, useEffect } from 'react';
import ReactApexChart from 'react-apexcharts';

const Home = () => {
    const [data, setData] = useState([30, 40, 35, 50, 49, 60, 70]);
    const [categories, setCategories] = useState(['January', 'February', 'March', 'April', 'May', 'June', 'July'])

    const handleDataChange = () => {
        // Xử lý khi dữ liệu thay đổi
    };

    const handleCategoryChange = () => {
        // Xử lý khi danh mục thay đổi
    };

    useEffect(() => {


    }, []);

    const state = {
        options: {
            chart: {
                id: 'basic-bar'
            },
            xaxis: {
                categories: categories
            }
        },
        series: [
            {
                name: 'NDK',
                data: data
            }
        ]
    };

    return (
        <div className='manager-home-content'>
            <div className='manager-chart'>
                <ReactApexChart options={state.options} series={state.series} type="line" height={710} width={900} />
            </div>
            <div className='manager-chart-controller' style={{ color: 'black' }}>
                
            </div>
        </div>
    );
};

export default Home;
