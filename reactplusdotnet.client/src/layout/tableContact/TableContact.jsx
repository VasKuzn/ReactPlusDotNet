import RowContact from "./components/RowContact"




const TableContact = (props) => {
  return (
      <table className="table table-hover">
          <thead>
              <tr>
                  <th>#</th>
                  <th>Contact name</th>
                  <th>Phone number</th>
                  <th>E-Mail</th>
              </tr>
          </thead>
          <tbody>
              {
                  props.contactlist.map(
                      contact =>
                          (<RowContact
                          id={contact.id}
                          name={contact.name}
                          phoneNumber={contact.phoneNumber}
                          email={contact.email}
                      />)
                  )
              }
          </tbody>
      </table>
  );
}

export default TableContact;