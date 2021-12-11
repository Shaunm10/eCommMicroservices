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
        Product? product = null;

        this.productRepositoryMock
            .Setup(x => x.GetProduct(productId))
            .ReturnsAsync(product);

        // act:
        var response = await this.controllerUnderTest.GetProductsById(productId);

        // assert:
        var notFoundResult = response?.Result as NotFoundResult;
        notFoundResult.Should().NotBeNull();
        this.loggerStub.VerifyLogging($"Product with id: {productId}, not found.", LogLevel.Error);
    }

    [Fact]
    public async Task ReturnProduct_From_GetProductsByCategory()
    {
        // arrange:
        var category = RandomValue.String();
        List<Product> products = new();
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

    [Fact]
    public async Task ReturnsResult_From_CreateProduct()
    {
        // arrange:
        var productId = RandomValue.String();
        var product =  new Product
        {
            Id = productId
        };

        // act:
        var result = await this.controllerUnderTest.CreateProduct(product);

        // asset:
        var createdAtRouteResult = result?.Result as CreatedAtRouteResult;
        createdAtRouteResult.Should().NotBeNull();
        createdAtRouteResult?.StatusCode.Should().Be(201);
        createdAtRouteResult?.RouteName.Should().Be("GetProducts");
        (createdAtRouteResult?.Value as Product)?.Id.Should().Be(productId);

    }

    [Fact]
    public async Task ReturnSuccess_From_UpdateProduct()
    {
        // arrange:
        var product = new Product();
        this.productRepositoryMock.Setup(x => x.UpdateProduct(product)).ReturnsAsync(true);

        // act:
        var response = await this.controllerUnderTest.UpdateProduct(product);

         // assert:
        var okResult = response as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.Value.Should().Be(true);
    }

    [Fact]
    public async Task ReturnSuccess_From_DeleteProduct()
    {
        // arrange:
        var productId = RandomValue.String();
        this.productRepositoryMock
            .Setup(x => x.DeleteProduct(productId))
            .ReturnsAsync(true);

        // act:
        var result = await this.controllerUnderTest.DeleteProductById(productId);

         // assert:
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult?.Value.Should().Be(true);
    }
}
