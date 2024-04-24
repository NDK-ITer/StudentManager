import { Routes, Route } from 'react-router-dom';
import CheckAdminRoute from './CheckAdminRoute';
import AdminPage from '../pages/AdminPage';
import ManagerPage from '../pages/ManagerPage';
import CheckManagerRoute from './CheckManagerRoute';
import UserPage from '../pages/UserPage';
import CheckStaffRoute from './CheckStaffRoute';
import StaffPage from '../pages/StaffPage';

const AppRoute = () => {
    return (<>
        <Routes>
            <Route path='/*' element={<UserPage/>} />
            <Route path='/admin/*' element={
                <CheckAdminRoute>
                    <AdminPage/>
                </CheckAdminRoute>
            } />
            <Route path='/manager/*' element={
                <CheckManagerRoute>
                    <ManagerPage/>
                </CheckManagerRoute>
            } />
            <Route path='/staff/*' element={
                <CheckStaffRoute>
                    <StaffPage/>
                </CheckStaffRoute>
            } />
        </Routes>
    </>)
}

export default AppRoute