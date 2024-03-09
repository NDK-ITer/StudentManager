import { Image, Form } from "react-bootstrap";
import MainLogo from '../../assets/images/main-logo.png'
import { Link } from "react-router-dom";

const Sidebar = ({ faculty, setState }) => {

    return (<>
        <div className="manager-sidebar">
            <div className="manager-text-logo">
                <Image
                    src={MainLogo}
                    style={{
                        marginTop: '10%',
                        width: '70%'
                    }}
                />
                <div style={{
                    margin: 'auto'
                }}>
                    {faculty.name}
                </div>
                <Form>
                    <Form.Check
                        type="switch"
                        id="custom-switch"
                        checked={faculty.isOpen}
                        onChange={() => setState()}
                    />
                </Form>
            </div>
            <div className="manager-option-list">
                <Link to='/manager' style={{ textDecoration: 'none', color: 'white' }}><div className="item"><i class="fa-solid fa-house"></i>&nbsp;Home</div></Link>
                <Link to='/manager/post' style={{ textDecoration: 'none', color: 'white' }}><div className="item"><i class="fa-solid fa-newspaper"></i>&nbsp;Post</div></Link>
                <Link to='/' style={{ textDecoration: 'none', color: 'white' }}><div className="item"><i class="fa-solid fa-arrow-right-from-bracket"></i>&nbsp;Back</div></Link>
            </div>
        </div>
    </>)
}
export default Sidebar;