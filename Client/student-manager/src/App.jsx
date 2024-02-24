import './assets/styles/App.scss';
import Container from 'react-bootstrap/Container';
import AppRoute from './routes/AppRoute';
import Header from './components/Header';
import Footer from './components/Footer';

function App() {
  return (
    <div className='app-container app'>
      <Header/>
      <Container>
        <AppRoute/>
      </Container>
      <Footer/>
    </div>
  );
}

export default App;
