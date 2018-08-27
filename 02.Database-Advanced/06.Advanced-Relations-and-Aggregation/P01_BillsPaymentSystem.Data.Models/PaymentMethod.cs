using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        public Type Type { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
        
        [Xor(nameof(BankAccountId))]
        public int? BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
        
        [Xor(nameof(CreditCardId))]
        public int? CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}