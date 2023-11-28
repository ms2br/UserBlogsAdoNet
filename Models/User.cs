namespace UsersBlogs.Models
{
    public record User
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string UserName { get; init; }
        public string UserEmailAddress { get; init; }
        public string Password { get; init; }
        public bool IsDeleted { get; init; }


        public override string ToString()
        {
            return $"{Id} {FirstName} {LastName} {UserName} {UserEmailAddress}";
        }
    }
}
