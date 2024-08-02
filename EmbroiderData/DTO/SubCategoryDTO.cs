namespace EmbroiderData.DTO
{
    public class SubCategoryDTO
    {
        public int Id { get; set; }

        public string SubCategoryCode { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryCode { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string GroupCode { get; set; }
    }
}
