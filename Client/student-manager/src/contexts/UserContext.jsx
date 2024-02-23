import {createContext, useState} from 'react'
import Cookies from 'js-cookie';

function IsLogin () {
    const jwt  = Cookies.get('jwt');
    const user = Cookies.get('user');
    if (!user && !jwt) {
        return false
    }
    return true
}

function GetUserName(){
    const user = Cookies.get('user')
    if (user) {
        return(JSON.parse(user).specialName)
    }else{
        return ''
    }
}

function GetUserAvatar(){
    const user = Cookies.get('user')
    if (user) {
        return(JSON.parse(user).linkAvatar)
    }else{
        return ''
    }
}

const UserContext = createContext({
    name: '',
    avatar:'',
    isAuth: false,
})

const UserProvider = ({ children }) =>{

    const [user, setUser] = useState({
        name: GetUserName(),
        avatar:GetUserAvatar(),
        isAuth: IsLogin(),
    })

    const loginContext = (response) => {
        setUser(() => ({
            name: response.data.specialName,
            avatar:response.data.linkAvatar,    
            isAuth: true,
        }))
        Cookies.set('jwt', response.jwt);
        Cookies.set('user', JSON.stringify(response.data));
    }

    const logout = () => {
        Cookies.remove('user')
        Cookies.remove('jwt')
        setUser(() => ({
            name: '',
            avatar:'',
            isAuth: false,
        }))
    }

    return (
        <UserContext.Provider value={{user, loginContext, logout}}>
            {children}
        </UserContext.Provider>
    )
}

export {
    UserContext,
    UserProvider
}