namespace BillSale.API.Models.Company
{
    /// <summary>
    /// Модель компании
    /// </summary>
    public class CompanyApiModel : CompanyCreateApiModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
