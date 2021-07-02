using System;

namespace Library.Entities
{
    public class Trustee
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
