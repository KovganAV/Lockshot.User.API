namespace Lockshot.User.API.Core.DTOs
{
    public class HitDto
    {
        public int UserId { get; set; }
        public string WeaponType { get; set; }
        public int Score { get; set; }
        public double Distance { get; set; }  
    }
}
