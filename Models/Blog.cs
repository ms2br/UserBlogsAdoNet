namespace UsersBlogs.Models
{
    public record Blog
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public int UserId { get; init; }
        public bool IsDeleted { get; init; }

        public override string ToString()
        {
            return $"{Id} {Title} {Description} {UserId}";
        }
    }
}
