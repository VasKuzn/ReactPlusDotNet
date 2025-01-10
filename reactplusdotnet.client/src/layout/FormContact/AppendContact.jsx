import FormContact from "./FormContact";
import axios from "axios";
import { useNavigate } from "react-router-dom";





const AppendContact = () => {
    const baseApiUrl = import.meta.env.VITE_APP_API_URL;
    const navigate = useNavigate();
    const addContact = (contactName, contactPhone, contactEmail) => {
        const item =
        {
            name: contactName,
            phoneNumber: contactPhone,
            email: contactEmail
        };
        let url = `${baseApiUrl}/contacts`;
        axios.post(url, item).then(() => { navigate("/") });
    }
  return (
      <div className="card">
          <div className="card-header">
          <h1>Add Contact</h1>
          </div>
          <div className = "card-body">
              <FormContact addContact={addContact} />
          </div>
      </div>
  );
}

export default AppendContact;