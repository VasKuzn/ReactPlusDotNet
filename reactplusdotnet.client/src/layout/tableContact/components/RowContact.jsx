import { Link } from "react-router-dom";

const RowContact = (props) => {
  return (
      <tr>
          <td>{props.id}</td>
          <td>{props.name}</td>
          <td>{props.phoneNumber}</td>
          <td>{props.email}</td>
          <td>
              <Link to={`/contact/${props.id}`} className = "btn btn-primary btn-sm"> Details</Link>
          </td>
      </tr>
  );
}

export default RowContact;