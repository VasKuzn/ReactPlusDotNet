using Bogus;
using Microsoft.Data.Sqlite;
using ReactPlusDotNet.Server.Models;

namespace ReactPlusDotNet.Server.Seed
{
    public class FakerInitializer : IInitializer
    {
        public string connectionString;

        public FakerInitializer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Initialize() 
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"CREATE TABLE IF NOT EXISTS contacts(
id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, phone TEXT NOT NULL, email TEXT NOT NULL);";
            command.ExecuteNonQuery();

            command.CommandText = @"SELECT count(*) FROM contacts;";

            long count = (long)command.ExecuteScalar();

            if (count == 0) 
            {
                var faker = new Faker<Contact>("ru").RuleFor(c => c.Name, f => f.Name.FullName()).RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber()).RuleFor(c => c.Email, f => f.Internet.Email());
                var contacts = faker.Generate(20);
                foreach (var contact in contacts) 
                {
                    command.CommandText = @"INSERT INTO contacts (name,phone,email) VALUES (@n,@p,@e);";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@n",contact.Name);
                    command.Parameters.AddWithValue("@p", contact.PhoneNumber);
                    command.Parameters.AddWithValue("@e", contact.Email);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
