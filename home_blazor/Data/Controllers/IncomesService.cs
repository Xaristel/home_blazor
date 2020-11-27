using home_blazor.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace home_blazor.Data.Controllers
{
    public class IncomesService
    {
        IUnitOfWork unitOfWork;

        public IncomesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Income> GetIncomeList()
        {
            return unitOfWork.GetIncomeRepository().GetAll();
        }

        public string GetSumForDay(string date)
        {
            int sum = 0;

            foreach (var item in unitOfWork.GetIncomeRepository().GetAll())
            {
                if (item.IncomeDate == date)
                {
                    sum += item.IncomeValue;
                }
            }

            return sum.ToString();
        }

        public string GetSumForMonth(string date)
        {
            int sum = 0;

            var list = from item in unitOfWork.GetIncomeRepository().GetAll()
                       where item.IncomeDate.EndsWith(date)
                       select item;

            foreach (var item in list)
            {
                sum += item.IncomeValue;
            }

            return sum.ToString();
        }

        public void AddNewItem(Income newIncome)
        {
            unitOfWork.RegisterNewObject(newIncome);
            unitOfWork.Commit();
        }

        public void DeleteItem(string inputDeleteName)
        {
            foreach (var item in unitOfWork.GetIncomeRepository().GetAll())
            {
                if (item.IncomeName.Trim() == inputDeleteName)
                {
                    unitOfWork.RegisterRemovedObject(item);
                }
            }
            unitOfWork.Commit();
        }
    }
}
