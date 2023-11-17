namespace ifood_core_api_7.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        Task CompleteAsync();

    }
}
