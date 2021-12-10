using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Api.Controllers;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RandomTestValues;
using Xunit;

namespace Catalog.Api.Tests.Controllers;


public class CatalogController_Should
{
    private readonly CatalogController controllerUnderTest;
    private readonly Mock<IProductRepository> productRepositoryMock;
    private readonly Mock<ILogger<CatalogController>> loggerStub;

    public CatalogController_Should()
    {
        this.productRepositoryMock = new Mock<IProductRepository>();
        this.loggerStub = new Mock<ILogger<CatalogController>>();
        this.controllerUnderTest = new CatalogController(this.productRepositoryMock.Object, this.loggerStub.Object);
    }

    [Fact]
    public async Task ReturnProducts_From_GetProducts()
    {
        // arrange:
        var products = new List<Product>();
        this.productRepositoryMock.Setup(x => x.GetProducts()).ReturnsAsync(products);

        // act:
        var response = await this.controllerUnderTest.GetProducts();

        // assert:
        var okResult = response?.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.Value.Should().BeSameAs(products);
    }

    [Fact]
    public async Task ReturnProduct_From_GetProductsById()
    {
          // arrange:
        var productId = RandomValue.String();
        var product = new Product();
        this.productRepositoryMock.Setup(x => x.GetProduct(productId)).ReturnsAsync(product);

        // act:
        var response = await this.controllerUnderTest.GetProductsById(productId);

        // assert:
        var okResult = response?.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.Value.Should().BeSameAs(product);
    }

     [Fact]
    public async Task ReturnNotFound_From_GetProductsById()
    {
          // arrange:
        var productId = RandomValue.String();
        var product = new Product();
        this.productRepositoryMock
            .Setup(x => x.GetProduct(productId))
            .ReturnsAsync((Product)null);

        // act:
        var response = await this.controllerUnderTest.GetProductsById(productId);

        // assert:
        var notFoundResult = response?.Result as NotFoundResult;
        notFoundResult.Should().NotBeNull();
        //this.loggerStub.Verify(x => x.LogError($"Product with id: {productId}, not found."), times: Times.Once);
        
    }

    [Fact]
    public async Task ReturnProduct_From_GetProductsByCategory()
    {
        // arrange:
        var category = RandomValue.String();
        var products =  new List<Product>();
        this.productRepositoryMock
            .Setup(x => x.GetProductsByCategory(category))
            .ReturnsAsync(products);

        // act:
        var response = await this.controllerUnderTest.GetProductsByCategory(category);
  
        // assert:
        var okResult = response?.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.Value.Should().BeSameAs(products);
    }

}
