namespace Fabrit.Heroes.Infrastructure.Common.Data
{
    /// <summary>
    /// Represents a database entity that has a primary key
    /// </summary>
    public interface IDataEntity
    {
        int Id { get; set; }
    }
}