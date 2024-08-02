namespace EmbroiderData.DTO
{
    public class EmbroiderDTO
    {
        #region Properties
        public int Id { get; set; }
        public string Address { get; set; }

        public decimal Balance { get; set; }

        public string EmbroiderCode { get; set; }

        public int InvoiceCount { get; set; }

        public string Name { get; set; }

        public decimal OpeningBalance { get; set; }

        public string Phone { get; set; }

        #endregion
    }
}
