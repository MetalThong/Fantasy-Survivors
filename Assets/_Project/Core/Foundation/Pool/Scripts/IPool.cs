namespace Core.Foundation.Pool
{
    public interface IPool<T> where T : class
    {
        T Get();
        void Release(T poolableObject);
        int ActiveCount { get; }
        int AvailableCount { get; }
    }
}