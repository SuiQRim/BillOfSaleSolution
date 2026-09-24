namespace BillSale.API.Models.Responces
{
    /// <summary>
    /// Модель содержающая ошибку
    /// </summary>
    public class ApiExceptionDetail
    {
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
