import React, { useState, useEffect } from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { UserProvider } from './contexts/UserContext';
import { BrowserRouter as Router} from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import '@fortawesome/fontawesome-free/css/all.css';
import "react-toastify/dist/ReactToastify.css";
import { RoleProvider } from './contexts/RoleContext';
import 'react-toastify/dist/ReactToastify.css';
import Loading from './components/Loading';

const withLoading = (WrappedComponent, loading) => {
  return loading ? <Loading /> : <WrappedComponent />;
}

const Root = () => {
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setTimeout(() => {
      setLoading(false);
    }, (1)*1000);
  }, []);

  return (
    <React.StrictMode>
      <RoleProvider>
        <UserProvider>
          <Router>
            {withLoading(App, loading)}
          </Router>
        </UserProvider>
      </RoleProvider>
    </React.StrictMode>
  );
}

ReactDOM.render(<Root />, document.getElementById('root'));

reportWebVitals();
