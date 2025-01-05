namespace WinBind.Domain.Models.User
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
