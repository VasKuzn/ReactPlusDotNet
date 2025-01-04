//import { useEffect, useState } from 'react';
import './App.css';
import TableContact from "./layout/tableContact/TableContact"

const contacts = [{ id: 1, name: "John", phoneNumber: 231321, email: "1@1.ru" },
{ id: 2, name: "Clara", phoneNumber: 543543, email: "2@2.ru" },
{ id: 3, name: "Vitya", phoneNumber: 786876, email: "3@3.ru" },
];


const App = () =>
{
    return(
        <div className="container mt-5">
            <div className = "card">
                <div className = "card-header">
                <h1>List of contacts</h1>
                </div>
                <div className="card-body">
                    <TableContact contactlist={contacts} />
                </div>
            </div>
        </div>
    );
}

export default App;