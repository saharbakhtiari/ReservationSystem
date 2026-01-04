using System;
using System.Collections.Generic;

namespace Application.Cartable.Queries.GetFilteredCartables
{
    public class FilteredCartablesDto
    {
        public long Id { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }

        public ICollection<FiltterdCartableProfileDto> Users { get; set; }
    }

    public class FiltterdCartableProfileDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }

}
