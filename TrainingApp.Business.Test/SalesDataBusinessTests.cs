using System;
using System.Linq;
using NUnit.Framework;
using TrainingApp.Data.Mock;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrainingApp.Shared.Interfaces;
using System.Collections.Generic;
using TrainingApp.Shared.DataModels;

namespace TrainingApp.Business.Test
{
    [TestFixture]
    public class BusinessProcessTests
    {
        private TrainingAppBusiness _businessLogic = null;
        private Mock<ISalesData> _moqProducts = null; 

        [OneTimeSetUp]
        public void TestsSetup()
        {
            _moqProducts = new Mock<ISalesData>();
            _moqProducts.Setup(sd => sd.GetProducts(It.IsAny<int>())).Returns((int subCategoryId) => this.GetProducts(subCategoryId));
            _businessLogic = new TrainingAppBusiness(_moqProducts.Object);
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(250)]
        public void GetProductsWithPriceEqualGreaterValue_ReturnsProductsWithPriceEqualGreaterValue(decimal lowPrice)
        {
            // Arrange
            var testProducts = this.GetProducts(0)
                                    .Where(pr => pr.Price >= lowPrice)
                                    .ToArray();

            // Act
            var productsFromBusinessLogicOverValue = _businessLogic.GetProducts(0, lowPrice).ToArray();

            // Assert
            Assert.That(productsFromBusinessLogicOverValue.Length, Is.EqualTo(testProducts.Length));
            Assert.That(productsFromBusinessLogicOverValue.Any(pr => pr.Price >= lowPrice), Is.EqualTo(true));
            Assert.That(productsFromBusinessLogicOverValue.Any(pr => pr.Price < lowPrice), Is.EqualTo(false));
        }

        [TestCase(100)]
        [TestCase(500)]
        [TestCase(2000)]
        public void GetProductsWithPriceEqualLessValue_ReturnsProductsWithPriceEqualLessValue(decimal highPrice)
        {
            // Arrange
            var testProducts = this.GetProducts(0)
                                    .Where(pr => pr.Price <= highPrice)
                                    .ToArray();

            // Act
            var productsFromBusinessLogicOverValue = _businessLogic.GetProducts(0, 0, highPrice).ToArray();

            // Assert
            Assert.That(productsFromBusinessLogicOverValue.Length, Is.EqualTo(testProducts.Length));
            Assert.That(productsFromBusinessLogicOverValue.Any(pr => pr.Price <= highPrice), Is.EqualTo(true));
            Assert.That(productsFromBusinessLogicOverValue.Any(pr => pr.Price > highPrice), Is.EqualTo(false));
        }


        private int ProductSubCategoryKeyRacer = 1;
        private int ProductSubCategoryKeyMountain = 2;
        private int ProductSubCategoryKeyRoad = 3;
        private int ProductSubCategoryKeyShortboard = 4;
        private int ProductSubCategoryKeyLongboard = 5;


        private IEnumerable<Product> GetProducts(int subcategoryId)
        {
            return GetTestProducts()
                    .Where(p => subcategoryId == 0 
                                || p.SubCategoryId == subcategoryId)
                    .ToArray();
        }

        private IEnumerable<Product> GetTestProducts()
        {
            var products = new List<Product>
            {
                new Product(10, ProductSubCategoryKeyRacer, "BX101W", "White", 599.95m),
                new Product(20, ProductSubCategoryKeyRacer, "BX101B", "Black", 599.95m),
                new Product(30, ProductSubCategoryKeyRacer, "BX101R", "Red", 649.95m),
                new Product(31, ProductSubCategoryKeyRacer, "BX101ER", "Red", 1000.00m),
                new Product(40, ProductSubCategoryKeyMountain, "MB401MB", "Black", 419.95m),
                new Product(50, ProductSubCategoryKeyMountain, "MB401MW", "White", 419.95m),
                new Product(60, ProductSubCategoryKeyMountain, "MB401WB", "Black", 500.005m),
                new Product(70, ProductSubCategoryKeyMountain, "MB401WW", "White", 519.95m),
                new Product(80, ProductSubCategoryKeyRoad, "RB512CW", "White", 319.95m),
                new Product(90, ProductSubCategoryKeyRoad, "RB512AW", "White", 419.95m),
                new Product(100, ProductSubCategoryKeyShortboard, "Tykester", "Black", 89.95m),
                new Product(110, ProductSubCategoryKeyShortboard, "BabyFreak", "Red", 99.95m),
                new Product(120, ProductSubCategoryKeyShortboard, "BabyFreak II", "Red", 100.00m),
                new Product(130, ProductSubCategoryKeyLongboard, "LongBeach", "White", 249.95m),
                new Product(140, ProductSubCategoryKeyLongboard, "Monster", "Flame", 349.95m),
            };

            return products.ToArray();
        }
    }
}
