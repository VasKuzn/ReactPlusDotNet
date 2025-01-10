import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";

const baseApiUrl = import.meta.env.VITE_APP_API_URL;

const ContactDetails = (props) => {
    const { id } = useParams();
    const [contact, setContact] = useState({ name: "", phoneNumber: "", email: "" });
    const navigate = useNavigate();

    useEffect(() => {
        const url = `${baseApiUrl}/contact/${id}`;
        axios.get(url)
            .then(response => setContact(response.data))
            .catch(err => {
                console.log(err);
                navigate("/");
            });
    }, [id, navigate]);

    const handleRemove = () =>
    {
        if (window.confirm("Are you sure?")) {
            const url = `${baseApiUrl}/contacts/${id}`;
            axios.delete(url).then(() => { navigate("/"); props.onUpdate(); }).catch(console.log("ошибочка"));
        }
    }

    const handleUpdate = () => {
        if (window.confirm("Are you sure?")) {
            const url = `${baseApiUrl}/contacts/${id}`;
            axios.put(url, contact).then(() => {
                navigate("/");
                props.onUpdate();
            }).catch(console.log("ошибочка"));
        }
    }


    return (
        <div className="container mt-5">
            <h2>Contact Details</h2>
            <div className="mb-3">
                <label className="form-label">Name: </label>
                <input className="form-control" type="text" value={contact.name} onChange={(e) => { setContact({ ...contact, name: e.target.value }); }} />
            </div>
            <div className="mb-3">
                <label className="form-label">Telephone: </label>
                <input className="form-control" type="text" value={contact.phoneNumber} onChange={(e) => { setContact({ ...contact, phoneNumber: e.target.value }); }} />
            </div>
            <div className="mb-3">
                <label className="form-label">Email: </label>
                <input className="form-control" type="text" value={contact.email} onChange={(e) => { setContact({ ...contact, email: e.target.value }); }} />
            </div>
            <button className="btn btn-primary me-2" onClick={() => {handleUpdate()}}>Update</button>
            <button className="btn btn-danger" onClick={() => { handleRemove()}}>Delete</button>
            <button className="btn btn-secondary ms-2" onClick={() => navigate("/")}>Back</button>
        </div>
    );
}

export default ContactDetails;