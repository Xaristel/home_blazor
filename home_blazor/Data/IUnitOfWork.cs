using home_blazor.Data.Model;

namespace home_blazor.Data
{
    public interface IUnitOfWork
    {
        IRepository<Income> GetIncomeRepository();
        IRepository<Cost> GetCostRepository();
        IRepository<Users> GetUsersRepository();

        void Commit();
        void RegisterNewObject(object obj);
        void RegisterDirtyObject(object obj);
        void RegisterRemovedObject(object obj);
    }
}
