//import { useEffect, useState } from 'react';
import './App.css';
import TableContact from "./layout/tableContact/TableContact"
import {useState} from "react"
const App = () =>
{
    const [contacts, SetContacts] = useState([{ id: 1, name: "John", phoneNumber: 231321, email: "1@1.ru" },
    { id: 2, name: "Clara", phoneNumber: 543543, email: "2@2.ru" },
    { id: 3, name: "Vitya", phoneNumber: 786876, email: "3@3.ru" },
    ]
    ); 
    const addContact = () => {
        const newID = contacts.sort((x, y) => x.id - y.id)[contacts.length - 1].id + 1;
        const item =
        {
            id: newID,
            name: "John",
            phoneNumber: 231321,
            email: "1@1.ru"
        };
        SetContacts([...contacts,item]);
    }

    return(
        <div className="container mt-5">
            <div className = "card">
                <div className = "card-header">
                <h1>List of contacts</h1>
                </div>
                <div className="card-body">
                    <TableContact contactlist={contacts} />
                </div>
                <div>
                    <button className="btn btn-primary"
                        onClick={() => {addContact()} }>Add
                    </button>
                </div>
            </div>
        </div>
    );
}

export default App;