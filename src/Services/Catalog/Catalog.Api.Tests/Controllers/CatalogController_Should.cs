using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api.Controllers;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
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
    public async Task GetProducts_From_GetProducts()
    {
        // arrange:
        var products = new List<Product>();
        this.productRepositoryMock.Setup(x => x.GetProducts()).ReturnsAsync(products);

        // act:
        var response = await this.controllerUnderTest.GetProducts();

        // assert:
        response.Should().NotBeNull();
        response.Result.Value.Should().BeSameAs(products);
    }


}
