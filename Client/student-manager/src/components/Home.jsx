import React from 'react';
import { Container } from 'react-bootstrap';
import MainBanner from './banner/MainBanner';
import '../assets/styles/HomePage.scss'

const HomePage = () => {
    return (
        <>
            <div className='banner-header'>
                <Container>
                    <div>
                        <MainBanner/>
                    </div>
                </Container>
            </div>
        </>
    );
};

export default HomePage;
