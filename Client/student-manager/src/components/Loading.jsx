import React from 'react';
import LoadingLogo from '../assets/images/logo-loading.png'
import { Image } from 'react-bootstrap';

const Loading = () => {
    return (
        <div className="loading-overlay">
            <div >
                <Image src={LoadingLogo} className="loading-spinner"/>
            </div>
        </div>
    );
}

export default Loading;
