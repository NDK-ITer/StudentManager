import { Routes, Route } from 'react-router-dom';
import CheckAdminRoute from '../CheckAdminRoute';
import Home from '../../components/admin/Home';
import User from '../../components/admin/components/User';
import UserDetail from '../../components/admin/components/UserDetail';
import Faculty from '../../components/admin/components/Faculty';

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
            <Route path='/faculty' element={
                <CheckAdminRoute>
                    <Faculty/>
                </CheckAdminRoute>
            } />
            <Route path='/user/:userId' element={
                <CheckAdminRoute>
                    <UserDetail/>
                </CheckAdminRoute>
            } />
        </Routes>
    </>)
}

export default AdminRoute