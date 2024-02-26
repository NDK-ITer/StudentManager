import './assets/styles/App.scss';
import Container from 'react-bootstrap/Container';
import AppRoute from './routes/AppRoute';
import Header from './components/Header';
import Footer from './components/Footer';
import { ToastContainer, toast } from "react-toastify";
import { useContext, useEffect } from 'react';
import { RoleContext } from './contexts/RoleContext';
import { GetAllRole } from './api/services/AuthService'
import { useLocation } from 'react-router-dom';

function App() {
  const { UpdateListRole } = useContext(RoleContext)

  const getRole = async () => {
    try {
      const res = await GetAllRole()
      if (res.State === 1) {
        UpdateListRole(res.Data.listRole)
      }
    } catch (error) {
      toast.error(error)
    }
  }

  const location = useLocation();
  const isAdminOrManagerPage = location.pathname.startsWith('/admin') || location.pathname.startsWith('/manager');

  useEffect(() => {
    getRole()
  }, [])
  return (
    <div className='app-container app'>
      <Container>
        {!isAdminOrManagerPage && <Header />}
        <AppRoute />
        {!isAdminOrManagerPage && <Footer />}
      </Container>
      <ToastContainer />
    </div>
  );
}

export default App;
