using System.Threading.Tasks;
using Basket.Api.Controllers;
using Basket.Api.Entities;
using Basket.Api.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

using RandomTestValues;
using Xunit;

namespace Basket.Api.Tests.Controllers
{
    public class BasketController_Should
    {
        private readonly BasketController controllerUnderTest;
        private readonly Mock<IBasketRepository> basketRepositoryMock;
        public BasketController_Should()
        {
            this.basketRepositoryMock = new Mock<IBasketRepository>();
            this.controllerUnderTest = new BasketController(this.basketRepositoryMock.Object);
        }

        [Fact]
        public async Task Return_New_Cart_From_GetBasket()
        {
            // arrange;
            var userName = RandomValue.String();

            // act:
            var response = await this.controllerUnderTest.GetBasket(userName);

            // asset:
            var okResult = response?.Result as OkObjectResult;

            var shoppingCart = okResult?.Value as ShoppingCart;
            shoppingCart.Should().NotBeNull();
            shoppingCart.UserName.Should().Be(userName);
            shoppingCart.Items.Count.Should().Be(0);
            shoppingCart.TotalPrice.Should().Be(0.0m);
        }

        Fact]
        public async Task Return_Existing_Cart_From_GetBasket()
        {
            // arrange;
            var userName = RandomValue.String();
            var cart = new ShoppingCart(userName);
            cart.Items.Add(new ShoppingCartItem
            {
                Color = RandomValue.String(),
                Price = RandomValue.Decimal(),
                ProductId = RandomValue.String(),
                ProductName = RandomValue.String()
            });

            this.basketRepositoryMock.Setup(x => x.GetBasket(userName)).ReturnsAsync(cart);

            // act:
            var response = await this.controllerUnderTest.GetBasket(userName);

            // asset:
            var okResult = response?.Result as OkObjectResult;

            var shoppingCart = okResult?.Value as ShoppingCart;
            shoppingCart.Should().NotBeNull();
            shoppingCart.UserName.Should().Be(userName);
            shoppingCart.Items.Count.Should().Be(1);
            shoppingCart.Should().Be(cart);
        }
    }
}