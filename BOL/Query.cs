using DAL;

namespace BOL
{
    public class Query : IModel
    {
        [ModelPropertyAttribute("MasterSearch", "select")]
        public string MasterSearch { get; set; }

        [ModelPropertyAttribute("Email", "select")]
        public string Email { get; set; }

        [ModelPropertyAttribute("eStatus", "select")]
        public int? eStatus { get; set; }
    }
}
