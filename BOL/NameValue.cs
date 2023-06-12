using DAL;

namespace BOL
{
    [ModelAttribute("NameValue", true)]
    public class NameValue : IModel
    {
        [ModelPropertyAttribute("NameValueId", "identity-select-delete-update")]
        public int? NameValueId { get; set; }

        [ModelPropertyAttribute("Name", "insert-select-delete-update")]
        public string Name { get; set; }

        [ModelPropertyAttribute("Value", "insert-update")]
        public string Value { get; set; }
    }
}
