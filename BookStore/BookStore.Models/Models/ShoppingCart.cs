﻿namespace BookStore.Models.Models
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
