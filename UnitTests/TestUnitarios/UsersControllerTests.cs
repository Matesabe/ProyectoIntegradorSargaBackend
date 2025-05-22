using Xunit;
        using Moq;
        using ProyectoIntegradorSarga.Controllers;
        using SharedUseCase.DTOs.User;
        using SharedUseCase.InterfacesUC;
        using Microsoft.AspNetCore.Mvc;
        using System.Collections.Generic;

        public class UsersControllerTests
        {
            [Fact]
            public void GetAll_ReturnsOkResult_WithListOfUsers()
            {
                // Arrange
                var mockGetAll = new Mock < IGetAll < UserDto >> ();
                mockGetAll.Setup(s => s.Execute()).Returns(new List<UserDto>());

                var controller = new UsersController(
                    mockGetAll.Object,
                    Mock.Of < IAdd < UserDto >> (),
                    Mock.Of < IRemove > (),
                    Mock.Of < IGetByName < UserDto >> (),
                    Mock.Of < IGetById < UserDto >> (),
                    Mock.Of < IUpdate < UserDto >> ()
                );

                // Act
                var result = controller.GetAll();

                // Assert
                var okResult = Assert.IsType < OkObjectResult > (result);
                Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
            }
        }
