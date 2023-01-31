using Microsoft.AspNetCore.Mvc.ModelBinding;



namespace Social_Network_API.Entities {
    public class User
    {

        public static int countOfUsers = 0;

        [BindNever]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [BindRequired]
        public string Name { get; set; } = string.Empty;
        [BindRequired]
        public string Email { get; set; } = string.Empty;
        [BindRequired]

        public int Age { get; set; }

        public readonly DateTime CreatedDate;
        public User(string name, string email, int age, DateTime createDate)
        {

            Name = name;
            Email = email;
            Age = age;
            CreatedDate = createDate;
            countOfUsers++;
        }
        public User() { }
    }
}