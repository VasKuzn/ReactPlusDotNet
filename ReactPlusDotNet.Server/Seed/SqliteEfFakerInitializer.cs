using Bogus;
using Microsoft.EntityFrameworkCore;
using ReactPlusDotNet.Server.DataContext;
using ReactPlusDotNet.Server.Models;

namespace ReactPlusDotNet.Server.Seed
{
    public class SqliteEfFakerInitializer : IInitializer
    {
        private readonly SqliteDbContext context;

        public SqliteEfFakerInitializer(SqliteDbContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
            context.Database.Migrate();
            if (!context.Contacts.Any())
            {
                var faker = new Faker<Contact>("ru")
                    .RuleFor(c => c.Name, f => f.Name.FullName())
                    .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Email, f => GenerateEmailForName(f.Name.FullName()));

                var contacts = faker.Generate(20);
                context.Contacts.AddRange(contacts);
                context.SaveChanges();
            }
        }

        private string GenerateEmailForName(string name) 
        {
            
            string email = Transliterate(name).ToLower().Replace(" ", ".") + "@example.com";
            return email;
        }
        private string Transliterate(string text)
        {
            var transliterationMap = new Dictionary<char, string>
        {
            {'А', "A"}, {'Б', "B"}, {'В', "V"}, {'Г', "G"}, {'Д', "D"}, {'Е', "E"}, {'Ё', "Yo"},
            {'Ж', "Zh"}, {'З', "Z"}, {'И', "I"}, {'Й', "Y"}, {'К', "K"}, {'Л', "L"}, {'М', "M"},
            {'Н', "N"}, {'О', "O"}, {'П', "P"}, {'Р', "R"}, {'С', "S"}, {'Т', "T"}, {'У', "U"},
            {'Ф', "F"}, {'Х', "Kh"}, {'Ц', "Ts"}, {'Ч', "Ch"}, {'Ш', "Sh"}, {'Щ', "Shch"}, {'Ъ', ""},
            {'Ы', "Y"}, {'Ь', ""}, {'Э', "E"}, {'Ю', "Yu"}, {'Я', "Ya"},
            {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"}, {'е', "e"}, {'ё', "yo"},
            {'ж', "zh"}, {'з', "z"}, {'и', "i"}, {'й', "y"}, {'к', "k"}, {'л', "l"}, {'м', "m"},
            {'н', "n"}, {'о', "o"}, {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"}, {'у', "u"},
            {'ф', "f"}, {'х', "kh"}, {'ц', "ts"}, {'ч', "ch"}, {'ш', "sh"}, {'щ', "shch"}, {'ъ', ""},
            {'ы', "y"}, {'ь', ""}, {'э', "e"}, {'ю', "yu"}, {'я', "ya"}
        };

            var result = new System.Text.StringBuilder();

            foreach (var ch in text)
            {
                if (transliterationMap.TryGetValue(ch, out var latin))
                {
                    result.Append(latin);
                }
                else
                {
                    result.Append(ch);
                }
            }

            return result.ToString();
        }
    }
}
