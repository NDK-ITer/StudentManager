import { Routes, Route } from 'react-router-dom';
import Home from '../../components/Home'
import Auth from '../../components/auth/Auth';
import AuthRoute from '../AuthRoute';
import UserInformation from '../../components/user/UserInformation';

const UserRoute = () => {
    return (<>
        <Routes>
            <Route path='/' element={<Home/>} />
            <Route path='/auth' element={<Auth />} />
            <Route path='/user-information' element={
                <AuthRoute>
                    <UserInformation />
                </AuthRoute>
            } />
        </Routes>
    </>)
}

export default UserRoute