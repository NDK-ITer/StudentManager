import UserHeader from './user/UserHeader';
import { Link } from 'react-router-dom';
import Image from 'react-bootstrap/Image';
import MainLogo from '../assets/images/main-logo.png'

const Header = () => {

    return (<>
        <div className='user-header'>
            <div className='main-logo'>
                <Link to='/'>
                    <Image src={MainLogo} style={{ width: '100%' }} />
                </Link>
            </div>
            <UserHeader />
        </div>
    </>)
}

export default Header