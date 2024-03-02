import AppRoute from './routes/AppRoute';
import { ToastContainer, toast } from "react-toastify";
import { useContext, useEffect } from 'react';
import { RoleContext } from './contexts/RoleContext';
import { GetAllRole } from './api/services/AuthService'
import './assets/styles/App.scss'
import { TransitionGroup, CSSTransition } from 'react-transition-group';

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
    <TransitionGroup>
      <CSSTransition timeout={300} classNames="fade">
        <AppRoute />
      </CSSTransition>
    </TransitionGroup>
    <ToastContainer />
  </>);
}

export default App;
