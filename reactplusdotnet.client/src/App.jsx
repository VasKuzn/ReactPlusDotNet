/* eslint-disable react/jsx-no-undef */
/* eslint-disable no-undef */
/* eslint-disable no-unused-vars */
//import { useEffect, useState } from 'react';
import './App.css';
import TableContact from "./layout/tableContact/TableContact"
import AppendContact from "./layout/FormContact/AppendContact"
import Pagination from "./layout/Pagination/Pagination"
import { useState, useEffect } from "react"
import axios from "axios";
import { Routes, Route, useLocation } from "react-router"
import ContactDetails from './layout/ContactDetails/ContactDetails';
import { Link } from "react-router-dom";

const baseApiUrl = import.meta.env.VITE_APP_API_URL;

const App = () =>
{
    
    const [contacts, SetContacts] = useState([]); 
    const location = useLocation();
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const pageSize = 5;
    const [updatetrigger, setUpdateTrigger] = useState(0);

    const handleUpdateTrigger = () =>
    {
        setUpdateTrigger(updateTrigger + 1);
    }

    const handlePageChange = (pageNumber) =>
    {
        setCurrentPage(pageNumber);
    }
    useEffect(() =>
    {
        const url = `${baseApiUrl}/contact/page?pageNumber=${currentPage}&pageSize=${pageSize}`;
        axios.get(url).then(res => {

            SetContacts(res.data.contacts)
            setTotalPages(Math.ceil(res.data.totalPages / pageSize));
        });
    }, [currentPage, pageSize,location.pathname]);

    
    return(
        <div className="container mt-5">
        <Routes>
                <Route path="/" element=
                    {
                    <div className="card">
                        <div className="card-header">
                            <h1>List of contacts</h1>
                        </div>
                        <div className="card-body">
                            <TableContact contactlist={contacts} />
                            <Pagination
                                currentPage={currentPage}
                                totalPages={totalPages}
                                onPageChange={handlePageChange}
                            />
                            <Link to = "/append" className = "btn btn-success mt-3"> Add contact</Link>

                        </div>
                    </div>
                    } />
                <Route path="contact/:id" element={<ContactDetails onUpdate={handleUpdateTrigger} />} />
                <Route path="append" element={<AppendContact />} />
        </Routes>
        </div>
        
    );
}

export default App;