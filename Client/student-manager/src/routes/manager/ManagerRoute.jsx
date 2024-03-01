import { Routes, Route } from 'react-router-dom';
import Home from '../../components/manager/Home';
import CheckManagerRoute from '../CheckManagerRoute';
import Post from '../../components/manager/components/Post';

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
        </Routes>
    </>)
}

export default ManagerRoute