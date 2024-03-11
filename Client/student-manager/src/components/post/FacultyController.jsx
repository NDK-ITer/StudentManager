
import { Form } from 'react-bootstrap';

const FacultyController = ({listFaculty, selectedFaculty, handleCheckboxChange}) => {


    return (<>
        <div>
            <div style={{
                fontFamily: 'fantasy',
                fontSize: '30px'
            }}>Faculty</div>
            <Form>
                {listFaculty && listFaculty.length > 0 && listFaculty.map((item, index) => {
                    return (<>
                        <Form.Check
                            className="element"
                            type='checkbox'
                            label={item.name}
                            onChange={() => handleCheckboxChange(item.id)}
                            checked={selectedFaculty.includes(item.id)}
                        />
                    </>)
                })}
            </Form>
        </div>
    </>)
}

export default FacultyController