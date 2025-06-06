namespace Yourdrs.Reports.API.Models;
public class Customer
{

    [Key]
    public Guid Id { get; set; }
    public string CustomerCode { get; set; } = default!;
    public string CustomerName { get; set; } = default!;

}