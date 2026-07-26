namespace BillSale.DAL.Contracts.Interfaces
{
    /// <summary>
    /// Сущность с идентификатором
    /// </summary>
    public interface IEntityWithId
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
