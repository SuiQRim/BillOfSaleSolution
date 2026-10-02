namespace BillSale.API.Tests.Infrastructure
{
    /// <summary>
    /// Атрибут коллекции тестов
    /// </summary>
    [CollectionDefinition(nameof(BillsalseApiTestCollection))]
    public class BillsalseApiTestCollection : ICollectionFixture<BillSaleApiFixture>;
}
