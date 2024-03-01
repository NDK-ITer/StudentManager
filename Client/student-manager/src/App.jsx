import AppRoute from './routes/AppRoute';
import { ToastContainer, toast } from "react-toastify";
import { useContext, useEffect } from 'react';
import { RoleContext } from './contexts/RoleContext';
import { GetAllRole } from './api/services/AuthService'
import './assets/styles/App.scss'

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
  return (<>
      <AppRoute />
      <ToastContainer />
  </>);
}

export default App;
