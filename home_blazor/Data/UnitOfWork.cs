using System;
using System.Collections.Generic;
using home_blazor.Data.Model;

namespace home_blazor.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DataContext context = new DataContext();
        private IRepository<Income> incomeRepository;
        private IRepository<Cost> costRepository;
        private IRepository<Users> usersRepository;

        private List<object> newObjects = new List<object>();
        private List<object> dirtyObjects = new List<object>();
        private List<object> removedObjects = new List<object>();

        public UnitOfWork(IRepository<Income> incomeRepository, IRepository<Cost> costRepository, IRepository<Users> usersRepository)
        {
            this.incomeRepository = incomeRepository;
            this.costRepository = costRepository;
            this.usersRepository = usersRepository;
        }

        public UnitOfWork()
        {
        }

        public IRepository<Income> GetIncomeRepository()
        {
            if (this.incomeRepository == null)
            {
                this.incomeRepository = new Repository<Income>(context);
            }
            return this.incomeRepository;
        }

        public IRepository<Cost> GetCostRepository()
        {
            if (this.costRepository == null)
            {
                this.costRepository = new Repository<Cost>(context);
            }
            return this.costRepository;
        }

        public IRepository<Users> GetUsersRepository()
        {
            if (this.usersRepository == null)
            {
                this.usersRepository = new Repository<Users>(context);
            }
            return this.usersRepository;
        }

        public void RegisterNewObject(object obj)
        {
            newObjects.Add(obj);
        }

        public void RegisterDirtyObject(object obj)
        {
            dirtyObjects.Add(obj);
        }

        public void RegisterRemovedObject(object obj)
        {
            removedObjects.Add(obj);
        }

        private void InsertNew()
        {
            foreach (var item in newObjects)
            {
                switch (item.GetType().Name.ToString())
                {
                    case "Income":
                        {
                            GetIncomeRepository().Add((Income)item);
                            break;
                        }
                    case "Cost":
                        {
                            GetCostRepository().Add((Cost)item);
                            break;
                        }
                    case "Users":
                        {
                            GetUsersRepository().Add((Users)item);
                            break;
                        }
                }
            }
        }

        private void DeletedRemoved()
        {
            foreach (var item in removedObjects)
            {
                switch (item.GetType().Name.ToString())
                {
                    case "Income":
                        {
                            GetIncomeRepository().Delete((Income)item);
                            break;
                        }
                    case "Cost":
                        {
                            GetCostRepository().Delete((Cost)item);
                            break;
                        }
                    case "Users":
                        {
                            GetUsersRepository().Delete((Users)item);
                            break;
                        }
                }
            }
        }

        private void UpdateDirty()
        {
            foreach (var item in dirtyObjects)
            {
                switch (item.GetType().Name.ToString())
                {
                    case "Income":
                        {
                            GetIncomeRepository().Update((Income)item);
                            break;
                        }
                    case "Cost":
                        {
                            GetCostRepository().Update((Cost)item);
                            break;
                        }
                    case "Users":
                        {
                            GetUsersRepository().Update((Users)item);
                            break;
                        }
                }
            }
        }

        public void Commit()
        {
            InsertNew();
            DeletedRemoved();
            UpdateDirty();
            context.SaveChanges();

            newObjects.Clear();
            removedObjects.Clear();
            dirtyObjects.Clear();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
