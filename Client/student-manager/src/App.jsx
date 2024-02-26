import './assets/styles/App.scss';
import Container from 'react-bootstrap/Container';
import AppRoute from './routes/AppRoute';
import { ToastContainer, toast } from "react-toastify";
import { useContext, useEffect } from 'react';
import { RoleContext } from './contexts/RoleContext';
import { GetAllRole } from './api/services/AuthService'

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
  useEffect(() => {
    getRole()
  }, [])
  return (
    <div className='app-container app'>
      <Container>
        <AppRoute />
      </Container>
      <ToastContainer />
    </div>
  );
}

export default App;
