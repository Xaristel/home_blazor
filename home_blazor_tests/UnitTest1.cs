using System;
using Xunit;
using home_blazor.Pages;
using home_blazor.Data.Model;
using home_blazor;
using Bunit;
using System.Collections.Generic;
using home_blazor.Data;
using Moq;
using home_blazor.Data.Controllers;
using System.Linq;

namespace home_blazor_tests
{
    public class UnitTest1
    {
        #region MockProperties
        public IRepository<Income> MockIncomeRepository;
        public IRepository<Cost> MockCostRepository;
        public IRepository<Users> MockUsersRepository;

        private List<Income> GetAllTestIncomes()
        {
            var Incomes = new List<Income>
            {
                new Income { IncomeName="Salary", IncomeId=1, IncomeValue=1000, IncomeDate="01.01.2020", UserId=1 },
                new Income { IncomeName="Sell", IncomeId=2, IncomeValue=2000, IncomeDate="01.01.2020", UserId=2},
                new Income { IncomeName="Sell", IncomeId=3, IncomeValue=3000, IncomeDate="03.02.2020", UserId=2},
                new Income { IncomeName="Sell", IncomeId=4, IncomeValue=4000, IncomeDate="04.02.2020", UserId=1}
            };
            return Incomes;
        }

        private List<Cost> GetAllTestCosts()
        {
            var Costs = new List<Cost>
            {
                new Cost { CostName="Products", CostId=1, CostValue=2000, CostDate="01.01.2020", UserId=1 },
                new Cost { CostName="Products", CostId=2, CostValue=2000, CostDate="01.01.2020", UserId=2},
                new Cost { CostName="Service", CostId=3, CostValue=3000, CostDate="03.02.2020", UserId=2},
                new Cost { CostName="Service", CostId=4, CostValue=3000, CostDate="04.02.2020", UserId=1}
            };
            return Costs;
        }

        private List<Users> GetUsers()
        {
            var Users = new List<Users>
            {
                new Users { UserId=1, UserName="Mikhail", UserPassword="1234"},
                new Users { UserId=2, UserName="Ivan", UserPassword="4321"}
            };
            return Users;
        }

        private void SetMockRepository()
        {
            Mock<IRepository<Income>> mockIncomeRepository = new Mock<IRepository<Income>>();
            List<Income> IncomesFull = GetAllTestIncomes();

            mockIncomeRepository.Setup(ir => ir.GetAll()).Returns(IncomesFull);
            this.MockIncomeRepository = mockIncomeRepository.Object;
            ////
            Mock<IRepository<Cost>> mockCostRepository = new Mock<IRepository<Cost>>();
            List<Cost> CostsFull = GetAllTestCosts();

            mockCostRepository.Setup(cr => cr.GetAll()).Returns(CostsFull);
            this.MockCostRepository = mockCostRepository.Object;
            ////
            Mock<IRepository<Users>> mockUsersRepository = new Mock<IRepository<Users>>();
            List<Users> Users = GetUsers();

            mockUsersRepository.Setup(cr => cr.GetAll()).Returns(Users);
            this.MockUsersRepository = mockUsersRepository.Object;
        }
        #endregion
        [Fact]
        public void TestCostsService()
        {
            SetMockRepository();

            UnitOfWork unitOfWork = new UnitOfWork(MockIncomeRepository, MockCostRepository, MockUsersRepository);
            CostsService costService = new CostsService(unitOfWork);

            var testListAll = costService.GetCostList();

            Assert.Equal(GetAllTestCosts().Count, testListAll.ToList().Count);

            string testSumDay = costService.GetSumForDay("01.01.2020");
            string testSumMonth = costService.GetSumForMonth("02.2020");

            Assert.Equal("4000", testSumDay);
            Assert.Equal("6000", testSumMonth);
        }

        [Fact]
        public void TestIncomesService()
        {
            SetMockRepository();

            UnitOfWork unitOfWork = new UnitOfWork(MockIncomeRepository, MockCostRepository, MockUsersRepository);
            IncomesService incomeService = new IncomesService(unitOfWork);

            var testListAll = incomeService.GetIncomeList();

            Assert.Equal(GetAllTestIncomes().Count, testListAll.ToList().Count);

            string testSumDay = incomeService.GetSumForDay("01.01.2020");
            string testSumMonth = incomeService.GetSumForMonth("02.2020");

            Assert.Equal("3000", testSumDay);
            Assert.Equal("7000", testSumMonth);
        }
    }
}
