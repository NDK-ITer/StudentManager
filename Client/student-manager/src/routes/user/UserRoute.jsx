import { Routes, Route } from 'react-router-dom';
import Home from '../../components/Home'
import Auth from '../../components/auth/Auth';
import AuthRoute from '../AuthRoute';
import UserInformation from '../../components/user/UserInformation';
import ListPost from '../../components/post/ListPost';
import DetailPost from '../../components/post/DetailPost';

const UserRoute = () => {
    return (<>
        <Routes>
            <Route path='/' element={<Home />} />
            <Route path='/auth' element={<Auth />} />
            <Route path='/user-information' element={
                <AuthRoute>
                    <UserInformation />
                </AuthRoute>
            } />
            <Route path='/post' element={
                <AuthRoute>
                    <ListPost />
                </AuthRoute>
            } />
            <Route path='/post/:postId' element={
                <AuthRoute>
                    <DetailPost />
                </AuthRoute>
            } />
        </Routes>
    </>)
}

export default UserRoute