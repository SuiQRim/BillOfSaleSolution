namespace BillSale.Common
{
    /// <summary>
    /// Класс хранящий информацию об ошибки валидации
    /// </summary>
    public class InvalidateItemModel
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="field">Поле</param>
        /// <param name="message">Сообщение</param>
        public InvalidateItemModel(string field, string message)
        {
            Field = field;
            Message = message;
        }

        /// <summary>
        /// Имя поля не прошедшего проверку
        /// </summary>
        /// <remarks>Если пустое, значит инвалидация относится ко всей моделе</remarks>
        public string Field { get; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; }

    }
}
