using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL
{
    [Model("Address", true)]
    public class Address : IModel
    {
        [ModelProperty("AddressId", "identity-select-delete-update")]
        public int? AddressId { get; set; }

        [ModelProperty("AddressName", "insert-select-delete-update")]
        public string AddressName { get; set; }

        [ModelProperty("eStatus", "insert-select-delete-update")]
        public int? eStatus { get; set; }

        [ModelProperty("InsertTime", "insert-select-delete-update")]
        public DateTime? InsertTime { get; set; }

        [ModelProperty("LastUpdateTime", "insert-select-delete-update")]
        public DateTime? LastUpdateTime { get; set; }
    }
}
