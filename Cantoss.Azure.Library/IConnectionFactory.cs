namespace Cantoss.Azure.Library
{
    /// <summary>
    /// Connection factory
    /// </summary>
    public interface IConnectionFactory
    {
        Task<T?> CreateConnection<T>(ConnectionType connectionType) where T : class;
    }
}