import { useEffect, useState } from "react"
import { GetFacultyPublic } from '../../api/services/FacultyService'
import { toast } from "react-toastify"
import { Form } from 'react-bootstrap';

const FacultyController = () => {

    const [listFaculty, setListFaculty] = useState([])
    const [selectedItems, setSelectedItems] = useState([]);

    const handleCheckboxChange = (id) => {
        const selectedIndex = selectedItems.indexOf(id);
        if (selectedIndex === -1) {
            setSelectedItems([...selectedItems, id]);
        } else {
            const updatedItems = [...selectedItems];
            updatedItems.splice(selectedIndex, 1);
            setSelectedItems(updatedItems);
        }
    };

    const getFacultyPublic = async () => {
        try {
            const res = await GetFacultyPublic();
            if (res.State === 1) {
                setListFaculty(res.Data.listFaculty)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    useEffect(() => {
        getFacultyPublic()
    }, [])
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
                            checked={selectedItems.includes(item.id)}
                        />
                    </>)
                })}
            </Form>
        </div>
    </>)
}

export default FacultyController