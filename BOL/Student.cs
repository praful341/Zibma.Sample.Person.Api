using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL
{
    [Model("Student", true)]
    public class Student : IModel
    {
        [ModelProperty("StudentId", "identity-select-delete-update")]
        public int? StudentId { get; set; }

        [ModelProperty("StudentName", "insert-select-delete-update")]
        public string StudentName { get; set; }

        [ModelProperty("FatherName", "insert-select-delete-update")]
        public string FatherName { get; set; }

        [ModelProperty("CityName", "insert-select-delete-update")]
        public string CityName { get; set; }

        [ModelProperty("Gender", "insert-select-delete-update")]
        public int? Gender { get; set; }

        [ModelProperty("Class", "insert-select-delete-update")]
        public string Class { get; set; }

        [ModelProperty("RoleNo", "insert-select-delete-update")]
        public string RoleNo { get; set; }

        [ModelProperty("Mobile", "insert-select-delete-update")]
        public string Mobile { get; set; }

        [ModelProperty("EmailAddress", "insert-select-delete-update")]
        public string EmailAddress { get; set; }

        [ModelProperty("SchoolFees", "insert-select-delete-update")]
        public decimal? SchoolFees { get; set; }

        [ModelProperty("BusFees", "insert-select-delete-update")]
        public decimal? BusFees { get; set; }

        [ModelProperty("Address", "insert-select-delete-update")]
        public string Address { get; set; }

        [ModelProperty("eStatus", "insert-select-delete-update")]
        public int? eStatus { get; set; }

        [ModelProperty("InsertTime", "insert-select-delete-update")]
        public DateTime? InsertTime { get; set; }

        [ModelProperty("LastUpdateTime", "insert-select-delete-update")]
        public DateTime? LastUpdateTime { get; set; }
    }
}
