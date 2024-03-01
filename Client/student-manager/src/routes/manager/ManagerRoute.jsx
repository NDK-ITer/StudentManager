import { Routes, Route } from 'react-router-dom';
import CheckAdminRoute from '../CheckAdminRoute';
import Home from '../../components/admin/Home';
import User from '../../components/admin/components/User';
import UserDetail from '../../components/admin/components/UserDetail';
import Faculty from '../../components/admin/components/Faculty';
import CheckManagerRoute from '../CheckManagerRoute';

const ManagerRoute = () => {
    return (<>
        <Routes>
            {/* <Route path='/' element={
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
                <CheckManagerRoute>
                    <Faculty/>
                </CheckManagerRoute>
            } />
            <Route path='/user/:userId' element={
                <CheckAdminRoute>
                    <UserDetail/>
                </CheckAdminRoute>
            } /> */}
        </Routes>
    </>)
}

export default ManagerRoute