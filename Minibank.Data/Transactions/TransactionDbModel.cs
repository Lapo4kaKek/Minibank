
namespace Minibank.Data.Transactions
{
    public class TransactionDbModel
    {
        public string Id { get; set; }
        public double Sum { get; set; }
        public string FromAccountId { get; set; }
        public string ToAccountId { get; set; }
    }
}
