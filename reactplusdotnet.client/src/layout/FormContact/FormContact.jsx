/* eslint-disable no-undef */
import { useState } from "react"

const FormContact = (props) =>
{
    const [contactName, setContactName] = useState("");
    const [contactPhone, setContactPhone] = useState("");
    const [contactEmail, setContactEmail] = useState("");

    const submit = () =>
    {
        if (contactName !== "" && contactPhone !== "" && contactEmail !== "")
        {
            props.addContact(contactName, contactPhone, contactEmail);
            setContactName("");
            setContactPhone("");
            setContactEmail("");
        }  
        else
        {
            alert("You have not filled all the data");
            return;
        }
    }
    return (
      <div>
          <div className="mb-3">
              <form>
                  <div className="mb-3">
                      <label className="form-label"> Write your name:</label>
                        <input className="form-control"
                            value={contactName}
                            onChange={(e) => { setContactName(e.target.value) }}
                          type="text"></input>
                  </div>
                  <div className="mb-3">
                      <label className="form-label"> Write your phone number:</label>
                        <input className="form-control"
                            value={contactPhone}
                            onChange={(e) => { setContactPhone(e.target.value) }}
                            type="text"></input>
                  </div>
                  <div className="mb-3">
                      <label className="form-label"> Write your e-mail:</label>
                        <input className="form-control"
                            value={contactEmail}
                            onChange={(e) => { setContactEmail(e.target.value) }}
                            type="text"></input>
                  </div>
              </form>
          </div>

          <div>
              <button className="btn btn-primary"
                    onClick={() => { submit(); }}>Add
              </button>
          </div>
      </div>
  );
}

export default FormContact;