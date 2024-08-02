namespace EmbroiderData.DTO
{
    public class EmbroiderInvoiceDetailDTO
    {
        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public string SubCategoryCode { get; set; }

        public int Id { get; set; }

        public int InvoiceId { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public EmbroiderInvoiceDetailType DetailType { get; set; }

        public int ActualQuantity { get; set; }

        public ActiveStatus ActiveStatus { get; set; }
    }
}
