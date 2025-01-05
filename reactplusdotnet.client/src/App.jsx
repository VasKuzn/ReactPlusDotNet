/* eslint-disable no-undef */
/* eslint-disable no-unused-vars */
//import { useEffect, useState } from 'react';
import './App.css';
import TableContact from "./layout/tableContact/TableContact"
import FormContact from "./layout/FormContact/FormContact"
import { useState, useEffect } from "react"
import axios from "axios";

const baseApiUrl = import.meta.env.VITE_APP_API_URL;

const App = () =>
{
    
    const [contacts, SetContacts] = useState([]); 


    const url = `${baseApiUrl}/contacts`;
    useEffect(() =>
    {
        axios.get(url).then(res => SetContacts(res.data));
    }, []);

    const addContact = (contactName, contactPhone, contactEmail) => {
        const newID = contacts.length === 0 ? 1 : contacts.sort((x, y) => x.id - y.id)[contacts.length - 1].id + 1;
        const item =
        {
            id: newID,
            name: contactName,
            phoneNumber: contactPhone,
            email: contactEmail
        };
        axios.post(url,item)
        SetContacts([...contacts,item]);
    }
    const deleteContact = (id) =>
    {
        const url = `${baseApiUrl}/contacts/${id}`;
        SetContacts(contacts.filter(item => item.id !== id));
        axios.delete(url);
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