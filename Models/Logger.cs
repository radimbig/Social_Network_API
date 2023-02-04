using Microsoft.AspNetCore.Mvc;
using Social_Network_API.Entities;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace Social_Network_API.Models
{
    public class Logger
    {

        private static FileStream LogStream;
        private static FileStream UsersStream;
        private StreamReader Reader;


        public Int64 countOfRequests
        {
            get
            {
                LogStream.Position = 0;
                string? temp = Reader.ReadLine();
                return Convert.ToInt64(temp);
            }


        }
        public Logger()
        {

            if (!Directory.Exists("Database"))
            {
                Directory.CreateDirectory("Database");
            }


            
            LogStream = new FileStream("Database\\CountOfRequests.txt", new FileStreamOptions()
            {
                Access = FileAccess.ReadWrite,
                Mode = FileMode.OpenOrCreate,
            });
            Reader = new StreamReader(LogStream);
            LogStream.Position = 0;
            string? temp = Reader.ReadLine();
            if (temp == null)
            {
                LogStream.Write(Encoding.Default.GetBytes("0"));
            }
            UsersStream = new FileStream("Database\\Users.txt", new FileStreamOptions
            {
                Access = FileAccess.ReadWrite,
                Mode = FileMode.OpenOrCreate
            });

        }
        public  void AddUser(User user)
        {
            UsersStream.Position = UsersStream.Length;
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user) + "\n";
            byte[] buffer = Encoding.Default.GetBytes(jsonUser);
            UsersStream.Write(buffer);
            UsersStream.Flush();
        }
        public void AddCountOfRequest()
        {
            LogStream.Position = 0;
            Int64 temp = Convert.ToInt64(Reader.ReadLine());
            temp++;
            byte[] buffer = Encoding.Default.GetBytes(temp.ToString());
            LogStream.Position = 0;
            LogStream.Write(buffer, 0, buffer.Length);
        }

    }
}