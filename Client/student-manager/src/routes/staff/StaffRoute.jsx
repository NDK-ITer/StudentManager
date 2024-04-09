import { Routes, Route } from 'react-router-dom';
import CheckStaffRoute from '../CheckStaffRoute';
import Home from '../../components/staff/Home';

const StaffRoute = () => {
    return (<>
        <Routes>
            <Route path='/' element={
                <CheckStaffRoute>
                    <Home/>
                </CheckStaffRoute>
            } />
        </Routes>
    </>)
}

export default StaffRoute