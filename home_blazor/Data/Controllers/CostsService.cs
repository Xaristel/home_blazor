using home_blazor.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace home_blazor.Data.Controllers
{
    public class CostsService
    {
        IUnitOfWork unitOfWork;

        public CostsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Cost> GetCostList()
        {
            return unitOfWork.GetCostRepository().GetAll();
        }

        public string GetSumForDay(string date)
        {
            int sum = 0;

            foreach (var item in unitOfWork.GetCostRepository().GetAll())
            {
                if (item.CostDate == date)
                {
                    sum += item.CostValue;
                }
            }

            return sum.ToString();
        }

        public string GetSumForMonth(string date)
        {
            int sum = 0;

            var list = from item in unitOfWork.GetCostRepository().GetAll()
                       where item.CostDate.EndsWith(date)
                       select item;

            foreach (var item in list)
            {
                sum += item.CostValue;
            }

            return sum.ToString();
        }

        public void AddNewItem(Cost newCost)
        {
            unitOfWork.RegisterNewObject(newCost);
            unitOfWork.Commit();
        }

        public void DeleteItem(string inputDeleteName)
        {
            foreach (var item in unitOfWork.GetCostRepository().GetAll())
            {
                if (item.CostName.Trim() == inputDeleteName)
                {
                    unitOfWork.RegisterRemovedObject(item);
                }
            }
            unitOfWork.Commit();
        }
    }
}
