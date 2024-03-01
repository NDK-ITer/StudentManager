import { useContext } from "react";
import { RoleContext } from "../../contexts/RoleContext";
import { Image } from "react-bootstrap";
import MainLogo from '../../assets/images/main-logo.png'

const Sidebar = () => {
    const { role } = useContext(RoleContext)
    return (<>
        <div className="manager-sidebar">
            <div className="manager-text-logo">
                <Image
                    src={MainLogo}
                    style={{
                        width: '70%'
                    }}
                />
                <div style={{
                    marginTop: '12%'
                }}>
                    {role.Manager.name}
                </div>
            </div>
        </div>
    </>)
}
export default Sidebar;