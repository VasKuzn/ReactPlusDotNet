using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using ReactPlusDotNet.Server.Interfaces;
using ReactPlusDotNet.Server.Models;
using ReactPlusDotNet.Server.ModelsDTO;

namespace ReactPlusDotNet.Server.Storage
{
    public class SqliteStorage : IStorage
    {
        private string connectionString;

        public SqliteStorage(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Contact AddContact(Contact contact)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                string cmd = "INSERT INTO contacts(name,phone,email) VALUES (@n, @p, @e); SELECT last_insert_rowid();";
                command.CommandText = cmd;
                command.Parameters.AddWithValue("@n", contact.Name);
                command.Parameters.AddWithValue("@p", contact.PhoneNumber);
                command.Parameters.AddWithValue("@e", contact.Email);
                command.ExecuteNonQuery();
                contact.Id = Convert.ToInt32(command.ExecuteScalar());
                return contact;
            }
        }

        public Contact FindContactById(int id)
        {
            var contact = new Contact();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM contacts WHERE id = @i;";
                command.Parameters.AddWithValue("@i", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contact = new Contact()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            PhoneNumber = reader.GetString(2),
                            Email = reader.GetString(3)
                        };
                    }
                }
            }
            return contact;
        }

        public List<Contact> GetAll()
        {
            var contact = new List<Contact>();
            using (var connection = new SqliteConnection(connectionString)) 
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM contacts;";
                using (var reader = command.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        contact.Add(new Contact()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            PhoneNumber = reader.GetString(2),
                            Email = reader.GetString(3)
                        });
                    }
                }
            }
            return contact;
        }

        public bool RemoveContact(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                string cmd = "DELETE FROM contacts WHERE id = @i;";
                command.CommandText = cmd;
                command.Parameters.AddWithValue("@i", id);
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateContact(int id, [FromBody] ContactDTO contactDTO)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                string cmd = "UPDATE contacts SET name = @n, phone = @p, email = @e WHERE id = @i;";
                command.CommandText = cmd;
                command.Parameters.AddWithValue("@i", id);
                command.Parameters.AddWithValue("@n", contactDTO.Name);
                command.Parameters.AddWithValue("@p", contactDTO.PhoneNumber);
                command.Parameters.AddWithValue("@e", contactDTO.Email);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
