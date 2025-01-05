/* eslint-disable no-unused-vars */
//import { useEffect, useState } from 'react';
import './App.css';
import TableContact from "./layout/tableContact/TableContact"
import FormContact from "./layout/FormContact/FormContact"
import {useState} from "react"
const App = () =>
{
    const [contacts, SetContacts] = useState([{ id: 1, name: "John", phoneNumber: 231321, email: "1@1.ru" },
    { id: 2, name: "Clara", phoneNumber: 543543, email: "2@2.ru" },
    { id: 3, name: "Vitya", phoneNumber: 786876, email: "3@3.ru" },
    ]
    ); 
    const addContact = (contactName, contactPhone, contactEmail) => {
        const newID = contacts.length === 0 ? 1 : contacts.sort((x, y) => x.id - y.id)[contacts.length - 1].id + 1;
        const item =
        {
            id: newID,
            name: contactName,
            phoneNumber: contactPhone,
            email: contactEmail
        };
        SetContacts([...contacts,item]);
    }
    const deleteContact = (id) =>
    {
        SetContacts(contacts.filter(item => item.id !== id));
    }
    return(
        <div className="container mt-5">
            <div className = "card">
                <div className = "card-header">
                <h1>List of contacts</h1>
                </div>
                <div className="card-body">
                    <TableContact contactlist={contacts}
                        deleteContact={deleteContact} />
                </div>
                <FormContact addContact={addContact} />
            </div>
        </div>
    );
}

export default App;