using FPTBook.Models;

namespace FPTLibrary.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        public Customer Customer { get; set; }
        public Book Book { get; set; }
    }
}
