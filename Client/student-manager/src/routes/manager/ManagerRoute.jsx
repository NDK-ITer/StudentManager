import { Routes, Route } from 'react-router-dom';
import Home from '../../components/manager/Home';
import CheckManagerRoute from '../CheckManagerRoute';
import Post from '../../components/manager/components/Post';
import PostDetail from '../../components/manager/components/PostDetail';

const ManagerRoute = () => {
    return (<>
        <Routes>
            <Route path='/' element={
                <CheckManagerRoute>
                    <Home/>
                </CheckManagerRoute>
            } />
            <Route path='/post' element={
                <CheckManagerRoute>
                    <Post/>
                </CheckManagerRoute>
            } />
            <Route path='/post/:postId' element={
                <CheckManagerRoute>
                    <PostDetail/>
                </CheckManagerRoute>
            } />
        </Routes>
    </>)
}

export default ManagerRoute