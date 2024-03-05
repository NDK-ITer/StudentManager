import { useContext, useEffect, useState } from "react"
import { UserContext } from '../../../contexts/UserContext'
import { toast } from "react-toastify"
import { GetPostOfFaculty } from "../../../api/services/PostService"
import { Image } from "react-bootstrap"
import { Link } from "react-router-dom"

const Post = () => {
    const { user } = useContext(UserContext)
    const [listPost, setListPost] = useState([])

    const updateListItemById = (id, propertyObj) => {
        const updatedListPost = [...listPost];
        const itemIndex = updatedListPost.findIndex(item => item.id === id);
        if (itemIndex === -1) {
            console.log(`Not found post with id: ${id}`);
            return;
        }
        Object.assign(updatedListPost[itemIndex], propertyObj);
        setListPost(updatedListPost);
    }

    const getListPost = async () => {
        try {
            const res = await GetPostOfFaculty(user.facultyId)
            if (res.State === 1) {
                setListPost(res.Data.listPost)
            } else {
                toast.warning(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    useEffect(() => {
        getListPost()
        console.log(listPost)
    }, [])

    return (<>
        <div className="manager-post">
            <div className="post-manager-controller">

            </div>
            <div className="list-post-manager">
                {listPost.length > 0 && listPost.map((item, index) => {
                    return (<>
                        <Link to={`/manager/post/${item.id}`} className="item">
                            <Image src={item.avatarPost} />
                            <div className={item.isCheck ? 'check' : 'no-check'}>
                                {item.title}
                            </div>
                        </Link>
                    </>)
                })}
            </div>
        </div>
    </>)
}

export default Post