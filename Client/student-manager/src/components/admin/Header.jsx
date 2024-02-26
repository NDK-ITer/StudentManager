import { Link } from "react-router-dom"
import { Image } from "react-bootstrap"
import MainLogo from "../../assets/images/main-logo.png"

const Header = ({ handleShow }) => {
    return (<>
        <div className="header">
            <div className="header-item">
                <div className="logo-admin">
                    <Link to='/admin'>
                        <Image src={MainLogo}/> 
                    </Link>
                </div>
            </div>
        </div>
    </>)
}
export default Header