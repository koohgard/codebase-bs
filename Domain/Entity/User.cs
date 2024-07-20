namespace Domain.Entity;
public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserType UserType { get; set; }
    public int LoginFaildCount { get; set; }
    public DateTime LockoutDateTime { get; set; }
    public virtual ICollection<Order> Orders { get; set; }

}

