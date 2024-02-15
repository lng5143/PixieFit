using PixieFit.Web.Business.Enums;

namespace PixieFit.Web.Business.Entities;

public class UserTransaction : BaseEntity
{
    public Guid UserId { get; set; }
    public UserTransactionType TransactionType { get; set; }
    public long CreditAmount { get; set; }
}