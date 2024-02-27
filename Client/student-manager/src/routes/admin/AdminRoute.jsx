import { Routes, Route } from 'react-router-dom';
import CheckAdminRoute from '../CheckAdminRoute';
import Home from '../../components/admin/Home';
import User from '../../components/admin/components/User';

const AdminRoute = () => {
    return (<>
        <Routes>
            <Route path='/' element={
                <CheckAdminRoute>
                    <Home/>
                </CheckAdminRoute>
            } />
            <Route path='/user' element={
                <CheckAdminRoute>
                    <User/>
                </CheckAdminRoute>
            } />
        </Routes>
    </>)
}

export default AdminRoute