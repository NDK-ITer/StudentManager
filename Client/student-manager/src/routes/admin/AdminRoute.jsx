import { Routes, Route } from 'react-router-dom';
import CheckAdminRoute from '../CheckAdminRoute';
import Home from '../../components/admin/Home';

const AdminRoute = () => {
    return (<>
        <Routes>
            <Route path='/' element={
                <CheckAdminRoute>
                    <Home/>
                </CheckAdminRoute>
            } />
        </Routes>
    </>)
}

export default AdminRoute