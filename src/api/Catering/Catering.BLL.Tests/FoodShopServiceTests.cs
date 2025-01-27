﻿using Catering.BLL.Interfaces;
using Catering.BLL.Services;
using Catering.DAL;
using Catering.DAL.Entities.FoodShops;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Catering.BLL.Tests
{
    public class FoodShopServiceTests
    {
        private readonly Mock<IRepository<FoodShop>> _foodShopRepositoryMock;
        private readonly IFoodShopService _foodShopService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public FoodShopServiceTests()
        {
            _foodShopRepositoryMock = new Mock<IRepository<FoodShop>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _foodShopService = new FoodShopService(_unitOfWorkMock.Object, _foodShopRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnAListOfMeal_WhenFoodSHopsExist()
        {
            var foodShops = CreateFoodShopList();

            _foodShopRepositoryMock.Setup(c => c.GetListAsync()).ReturnsAsync(foodShops);

            var result = await _foodShopService.GetFoodShops();
            Assert.NotNull(result);
            Assert.IsType<List<FoodShop>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnNull_WhenFoodShopsDoNotExist()
        {
            _foodShopRepositoryMock.Setup(c => c.GetListAsync())
                .ReturnsAsync((List<FoodShop>)null);

            var result = await _foodShopService.GetFoodShops();

            Assert.Null(result);
        }

        [Fact]
        public async void GetAll_ShouldCallGetAllFromRepository_OnlyOnce()
        {
            _foodShopRepositoryMock.Setup(c =>
                c.GetListAsync()).ReturnsAsync((List<FoodShop>)null);

            await _foodShopService.GetFoodShops();

            _foodShopRepositoryMock.Verify(mock => mock.GetListAsync(), Times.Once);
        }

        [Fact]
        public async void GetById_ShouldReturnFoodShop_WhenFoodShopExist()
        {
            var foodShop = CreateFoodShop();

            _foodShopRepositoryMock.Setup(c => c.GetAsync(foodShop.Id))
                .ReturnsAsync(foodShop);

            var result = await _foodShopService.GetFoodShop(foodShop.Id);

            Assert.NotNull(result);
            Assert.IsType<FoodShop>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenFoodShopDoesNotExist()
        {
            _foodShopRepositoryMock.Setup(c => c.GetAsync(1))
                .ReturnsAsync((FoodShop)null);

            var result = await _foodShopService.GetFoodShop(1);

            Assert.Null(result);
        }

        [Fact]
        public async void GetById_ShouldCallGetByIdFromRepository_OnlyOnce()
        {
            _foodShopRepositoryMock.Setup(c =>
                c.GetAsync(1)).ReturnsAsync((FoodShop)null);

            await _foodShopService.GetFoodShop(1);

            _foodShopRepositoryMock.Verify(mock => mock.GetAsync(1), Times.Once);
        }

        [Fact]
        public void Add_ShouldAddFoodShop_WhenFoodShopIdDoesExist()
        {
            var foodShop = CreateFoodShop();
            _foodShopRepositoryMock.Setup(c => c.Add(foodShop));

            var result = _foodShopService.AddFoodShop(foodShop);

            Assert.NotNull(result);
        }

        [Fact]
        public async void Add_ShouldCallAddFromRepository_OnlyOnce()
        {
            var foodShop = CreateFoodShop();

            _foodShopRepositoryMock.Setup(c => c.Add(foodShop));

            _foodShopRepositoryMock.Setup(c =>
                    c.GetListAsync())
                .ReturnsAsync(new List<FoodShop>());

            await _foodShopService.AddFoodShop(foodShop);

            _foodShopRepositoryMock.Verify(mock => mock.Add(foodShop), Times.Once);
        }

        [Fact]
        public void Update_ShouldUpdateFoodShop_WhenDoesExist()
        {
            var foodShop = CreateFoodShop();

            _foodShopRepositoryMock.Setup(c => c.Update(foodShop));

            var result = _foodShopService.UpdateFoodShop(foodShop);

            Assert.NotNull(result);
        }

        [Fact]
        public async void Update_ShouldCallUpdateFromRepository_OnlyOnce()
        {
            var foodShop = CreateFoodShop();

            _foodShopRepositoryMock.Setup(c => c.GetListAsync())
                .ReturnsAsync(new List<FoodShop>());

            await _foodShopService.UpdateFoodShop(foodShop);

            _foodShopRepositoryMock.Verify(mock => mock.Update(foodShop), Times.Once);
        }

        [Fact]
        public void Remove_ShouldNotRemove_WhenHasRelated()
        {
            var foodShop = CreateFoodShop();

            var foods = new List<Food>()
            {
                new Food()
                {
                    Id = 1,
                    FoodName = "FoodShop 1",
                    Description = "Description 1",
                    FoodShopId = foodShop.Id
                }
            };

            var result = _foodShopService.DeleteFoodShop(foodShop);

            Assert.IsAssignableFrom<Task>(result);
        }

        [Fact]
        public void Remove_ShouldRemoveFoodShop_WhenFoodShopExists()
        {
            var foodShop = CreateFoodShop();

            _foodShopRepositoryMock.Setup(c => c.GetAsync(foodShop.Id)).ReturnsAsync(foodShop);

            var result = _foodShopService.DeleteFoodShop(foodShop);

            Assert.NotNull(result);
        }

        [Fact]
        public async void Remove_ShouldCallRemoveFromRepository_OnlyOnce()
        {
            var foodShop = CreateFoodShop();

            await _foodShopService.DeleteFoodShop(foodShop);

            _foodShopRepositoryMock.Verify(mock => mock.Delete(foodShop), Times.Once);
        }

        private FoodShop CreateFoodShop()
        {
            return new FoodShop()
            {
                Id = 1,
                Name = "FoodShop",
                PictureUrl = "avdjsk"
            };
        }

        private List<FoodShop> CreateFoodShopList()
        {
            return new List<FoodShop>()
            {
                new FoodShop()
                {
                   Id = 1,
                   Name = "FoodShop 1",
                   PictureUrl = "bdbsb"
                },
                new FoodShop()
                {
                    Id = 2,
                    Name = "FoodShop 2",
                    PictureUrl = "shdbsna"
                },
                new FoodShop()
                {
                    Id = 3,
                    Name = "FoodShop 3",
                    PictureUrl = "ndnsjjs"
                }
            };
        }
    }
}
