using Application.Cartable.Queries.GetFilteredCartables;
using System;
using System.Collections.Generic;

namespace Application.Cartable.Queries.GetCartableById
{

    public class CartableDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public ICollection<CartableProfileDto> Users { get; set; }
    }
    public class CartableProfileDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }

}
