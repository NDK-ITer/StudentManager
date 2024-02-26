
const Header = ({ handleShow }) => {
    return (<>
        <div className="header">
            <div className="option-list">
                <div onClick={handleShow}>
                    Option
                </div>
                <div onClick={handleShow}>
                    Header
                </div>
            </div>
        </div>
    </>)
}
export default Header