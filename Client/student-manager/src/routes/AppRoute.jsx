import {Routes, Route} from 'react-router-dom';
import HomePage from '../components/Home'
import Auth from '../components/auth/Auth';
import AuthRoute from './AuthRoute';
import UserInformation from '../components/user/UserInformation';

const AppRoute = () => {
    return(<>
        <Routes>
            <Route path='/' element={<HomePage/>}/>
            <Route path='/auth' element={<Auth/>}/>
            <Route path='/user-information' element={
                <AuthRoute>
                    <UserInformation/>
                </AuthRoute>
            }/>
        </Routes>
    </>)
}

export default AppRoute