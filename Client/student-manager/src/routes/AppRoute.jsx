import { Routes, Route } from 'react-router-dom';
import HomePage from '../components/Home'
import Auth from '../components/auth/Auth';
import AuthRoute from './AuthRoute';
import UserInformation from '../components/user/UserInformation';
import CheckAdminRoute from './CheckAdminRoute';
import AdminPage from '../pages/AdminPage';
import ManagerPage from '../pages/ManagerPage';

const AppRoute = () => {
    return (<>
        <Routes>
            <Route path='/' element={<HomePage />} />
            <Route path='/auth' element={<Auth />} />
            <Route path='/user-information' element={
                <AuthRoute>
                    <UserInformation />
                </AuthRoute>
            } />
            <Route path='/admin' element={
                <CheckAdminRoute>
                    <AdminPage/>
                </CheckAdminRoute>
            } />
            <Route path='/manager' element={
                <CheckAdminRoute>
                    <ManagerPage/>
                </CheckAdminRoute>
            } />
        </Routes>
    </>)
}

export default AppRoute