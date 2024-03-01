import { Routes, Route } from 'react-router-dom';
import Home from '../../components/manager/Home';
import CheckManagerRoute from '../CheckManagerRoute';

const ManagerRoute = () => {
    return (<>
        <Routes>
            <Route path='/' element={
                <CheckManagerRoute>
                    <Home/>
                </CheckManagerRoute>
            } />
        </Routes>
    </>)
}

export default ManagerRoute