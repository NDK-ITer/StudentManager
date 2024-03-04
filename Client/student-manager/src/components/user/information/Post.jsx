import { useEffect, useState } from "react"
import { UploadPost, GetMyPost } from "../../../api/services/PostService"
import {GetFacultyPublic} from '../../../api/services/FacultyService'
import { toast } from "react-toastify"
import {Image} from 'react-bootstrap'

const Post = ({userId}) =>{

    const [listPost, setListPost] = useState([])
    const [postData, setPostData] = useState()
    const [listFaculty, setListFaculty] = useState([])

    const addNewPost = (newElement) => {
        const newListPost = [...listPost];
        newListPost.push(newElement);
        setListPost(newListPost);
    }

    const uploadPost = async () =>{
        try {
            const res = await UploadPost();
            if(res.State === 1){
                addNewPost(res.Data)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const getMyListPost = async() =>{
        try {
            const res = await GetMyPost();
            if(res.State === 1){
                setListPost(res.Data.listPost)
            }else{
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    const getListFaculty = async() =>{
        try {
            const res = await GetFacultyPublic();
            if(res.State === 1){
                setListFaculty(res.Data.listFaculty)
            }else{
                toast.error(res.Data.mess)
            }
        } catch (error) {
            toast.error(error)
        }
    }

    useEffect(()=>{
        getMyListPost()
        getListFaculty()
    },[])
    return(<>
        <div className="my-post-area">
            <div className="controller-my-post">

            </div>
            <div className="list-post">
                {(listPost && listPost.length >0) ? listPost.map((item, index)=> {
                    return(<div key={item.id} className="list-post item">
                        <Image src={item.avatarPost}/>
                    </div>)
                }):<div> No Post ! </div>}
            </div>
        </div>
    </>)
}

export default Post