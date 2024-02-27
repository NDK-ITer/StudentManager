import { Routes, Route } from 'react-router-dom';
import CheckAdminRoute from './CheckAdminRoute';
import AdminPage from '../pages/AdminPage';
import ManagerPage from '../pages/ManagerPage';
import CheckManagerRoute from './CheckManagerRoute';
import UserPage from '../pages/UserPage';
import DocxReader from '../components/DocReader';

const AppRoute = () => {
    return (<>
        <Routes>
            <Route path='/*' element={<UserPage/>} />
            <Route path='/admin/*' element={
                <CheckAdminRoute>
                    <AdminPage/>
                </CheckAdminRoute>
            } />
            <Route path='/manager/*' element={
                <CheckManagerRoute>
                    <ManagerPage/>
                </CheckManagerRoute>
            } />
            <Route path='/test' element={<DocxReader/>}/>
        </Routes>
    </>)
}

export default AppRoute