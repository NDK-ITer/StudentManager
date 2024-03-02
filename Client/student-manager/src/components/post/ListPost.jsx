import DateController from "./DateController"
import FacultyController from "./FacultyController"


const ListPost = () => {
    return (<>
        <div className="list-post-content">
            <div className="faculty-controller">
                <FacultyController/>
            </div>
            <div className="list-post-element">
                List Post
            </div>
            <div className="date-post-controller">
                <DateController/>
            </div>
        </div>
    </>)
}

export default ListPost