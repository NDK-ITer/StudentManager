import { createContext, useState } from 'react'


const RoleContext = createContext([])

const RoleProvider = ({ children }) => {

    const [role, setRole] = useState({
        Admin: {
            role: '',
            name: '',
        },
        Manager: {
            role: '',
            name: '',
        },
        User: {
            role: '',
            name: '',
        },
        Staff: {
            role: '',
            name: ''
        },
        Student: {
            role: '',
            name: ''
        }
    })

    const UpdateListRole = (props) => {
        props.forEach(item => {
            if (item.name === 'ADMIN') {
                setRole(prevState => ({
                    ...prevState,
                    Admin: {
                        role: item.name,
                        name: item.normalizeName,
                    },
                }));
            }
            if (item.name === 'MANAGER') {
                setRole(prevState => ({
                    ...prevState,
                    Manager: {
                        role: item.name,
                        name: item.normalizeName,
                    },
                }));
            }
            if (item.name === 'USER') {
                setRole(prevState => ({
                    ...prevState,
                    User: {
                        role: item.name,
                        name: item.normalizeName,
                    },
                }));
            }
            if (item.name === 'STAFF') {
                setRole(prevState => ({
                    ...prevState,
                    Staff: {
                        role: item.name,
                        name: item.normalizeName,
                    },
                }));
            }
            if (item.name === 'STUDENT') {
                setRole(prevState => ({
                    ...prevState,
                    Student: {
                        role: item.name,
                        name: item.normalizeName,
                    },
                }));
            }
        });
    }

    return (
        <RoleContext.Provider value={{
            role,
            UpdateListRole
        }}>
            {children}
        </RoleContext.Provider>
    )
}

export {
    RoleContext,
    RoleProvider,
}