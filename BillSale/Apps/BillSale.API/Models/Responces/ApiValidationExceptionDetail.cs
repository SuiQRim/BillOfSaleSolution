using BillSale.Common;

namespace BillSale.API.Models.Responces
{
    /// <summary>
    /// Модель содердающая ошибки
    /// </summary>
    public class ApiValidationExceptionDetail
    {
        /// <summary>
        /// Ошибки валидации
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; set; } = Array.Empty<InvalidateItemModel>();
    }
}


