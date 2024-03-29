import { createContext, useState } from 'react'
import Cookies from 'js-cookie';

function IsLogin() {
    const jwt = Cookies.get('jwt');
    const user = Cookies.get('user');
    if (!user && !jwt) {
        return false
    }
    return true
}

function GetUserName() {
    const user = Cookies.get('user')
    if (user) {
        return (JSON.parse(user).userName)
    } else {
        return ''
    }
}

function GetUserAvatar() {
    const user = Cookies.get('user')
    if (user) {
        return (JSON.parse(user).linkAvatar)
    } else {
        return ''
    }
}

function GetUserFirstName() {
    const user = Cookies.get('user')
    if (user) {
        return (JSON.parse(user).firstName)
    } else {
        return ''
    }
}

function GetUserLastName() {
    const user = Cookies.get('user')
    if (user) {
        return (JSON.parse(user).lastName)
    } else {
        return ''
    }
}

function GetUserEmail() {
    const user = Cookies.get('user')
    if (user) {
        return (JSON.parse(user).email)
    } else {
        return ''
    }
}

function GetUserRole() {
    const user = Cookies.get('user')
    if (user) {
        return (JSON.parse(user).role)
    } else {
        return ''
    }
}

function GetFacultyId() {
    const user = Cookies.get('user')
    if (user) {
        return (JSON.parse(user).facultyId)
    } else {
        return undefined
    }
}

const UserContext = createContext({
    userName: '',
    avatar: '',
    firstName: '',
    lastName: '',
    email: '',
    role: '',
    facultyId: undefined,
    isAuth: false,
})

const UserProvider = ({ children }) => {

    const [randomParam, setRandomParam] = useState(Math.random());
    const [user, setUser] = useState({
        userName: GetUserName(),
        avatar: GetUserAvatar(),
        firstName: GetUserFirstName(),
        lastName: GetUserLastName(),
        email: GetUserEmail(),
        role: GetUserRole(),
        facultyId: GetFacultyId(),
        isAuth: IsLogin(),
    })

    const UpdateUser =(props) =>{
        let user = JSON.parse(Cookies.get('user'))
        if (props.userName) {
            user.userName = props.userName
        }
        if (props.firstName) {
            user.firstName = props.firstName
        }
        if (props.lastName) {
            user.lastName = props.lastName
        }
        if (props.email) {
            user.email = props.email
        }
        Cookies.set('user', JSON.stringify(user));
        UpdateUserFromCookie()
    }

    const loginContext = (response) => {
        let userData = {
            userName: response.Data.userName,
            avatar: response.Data.linkAvatar,
            firstName: response.Data.firstName,
            lastName: response.Data.lastName,
            email: response.Data.email,
            role: response.Data.role,
            isAuth: true,
        }
        if (response.Data.facultyId) {
            userData.facultyId = response.Data.facultyId
        }
        console.log(userData)
        setUser(() => (userData))
        Cookies.set('jwt', response.jwt);
        Cookies.set('user', JSON.stringify(response.Data));
    }

    const logout = () => {
        Cookies.remove('user')
        Cookies.remove('jwt')
        setUser(() => ({
            userName: '',
            avatar: '',
            isAuth: false,
        }))
    }

    const UpdateUserFromCookie = () => {
        setUser({
            userName: GetUserName(),
            firstName: GetUserFirstName(),
            lastName: GetUserLastName(),
            email: GetUserEmail(),
            role: GetUserRole(),
            isAuth: IsLogin(),
            avatar: GetUserAvatar()
        })
    }

    const refreshImage = () => {
        setRandomParam(Math.random())
    }

    return (
        <UserContext.Provider value={{
            user,
            loginContext,
            logout,
            randomParam,
            refreshImage,
            UpdateUserFromCookie,
            UpdateUser
        }}>
            {children}
        </UserContext.Provider>
    )
}

export {
    UserContext,
    UserProvider,
}