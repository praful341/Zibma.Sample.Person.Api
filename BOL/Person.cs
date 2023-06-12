using DAL;
using System;

namespace BOL
{
    [Model("Person", true)]
    public class Person : IModel
    {
        [ModelProperty("PersonId", "identity-select-delete-update")]
        public int? PersonId { get; set; }

        [ModelProperty("FirstName", "insert-select-delete-update")]
        public string FirstName { get; set; }

        [ModelProperty("LastName", "insert-select-delete-update")]
        public string LastName { get; set; }

        [ModelProperty("Mobile", "insert-select-delete-update")]
        public string Mobile { get; set; }

        [ModelProperty("Email", "insert-select-delete-update")]
        public string Email { get; set; }

        [ModelProperty("Gender", "insert-select-delete-update")]
        public int? Gender { get; set; }

        [ModelProperty("eStatus", "insert-select-delete-update")]
        public int? eStatus { get; set; }

        [ModelProperty("InsertTime", "insert-select-delete-update")]
        public DateTime? InsertTime { get; set; }
        
        [ModelProperty("LastUpdateTime", "insert-select-delete-update")]
        public DateTime? LastUpdateTime { get; set; }

    }
}
