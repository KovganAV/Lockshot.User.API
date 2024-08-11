namespace Lockshot.User.API.Class
{
    public class Hit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string WeaponType { get; set; }
        public int Score { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
