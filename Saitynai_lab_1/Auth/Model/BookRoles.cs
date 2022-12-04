namespace Saitynai_lab_1.Auth.Model
{
    public static class BookRoles
    {
        public const string Admin = nameof(Admin);
        public const string ForumUser = nameof(ForumUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, ForumUser };
    }
}
