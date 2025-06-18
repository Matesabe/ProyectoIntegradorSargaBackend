using BusinessLogic.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProyectoIntegradorSarga.Controllers;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;
using System.Collections.Generic;
using Xunit;

namespace xUnitTests
{
    public class UsersControllerTests
    {
        // Reemplaza int.any por cualquier valor entero válido, por ejemplo 1
        UserDto userOk = new UserDto(1, "12345678", "Juan Pérez", "juan.perez@email.com", "Pass123", "600123456", "Administrator");

        UserDto userRepetido = new UserDto(0, "12345678", "Juan Pérez", "juan.perez@email.com", "Pass123", "600123456", "Administrator");

        UserDto userCIErrorLength = new UserDto(0, "123456789", "Ana García", "ana.garcia@email.com", "Pass456", "600654321", "Client");
        UserDto userCIErrorNum = new UserDto(0, "1234567B", "Ana García", "ana.garcia@email.com", "Pass456", "600654321", "Client");

        UserDto userEmailError = new UserDto(0, "12345678", "Ana García", "ana.garciaemail.com", "Pass456", "600654321", "Client");

        UserDto userPassError = new UserDto(0, "12345678", "Ana García", "ana.garcia@email.com", "123", "600654321", "Client");

        UserDto userPhoneError = new UserDto(0, "12345678", "Ana García", "ana.garcia@email.com", "Pass456", "60065A321", "Client");

        UserDto userRoleError = new UserDto(0, "12345678", "Ana García", "ana.garcia@email.com", "Pass456", "600654321", "InvalidRole");


        public UsersControllerTests() { }

        [Fact]
        public void GetAll_ReturnsOkResult()
        {
            // Arrange  
            var mockGetAll = new Mock<IGetAll<UserDto>>();
            mockGetAll.Setup(s => s.Execute()).Returns(new List<UserDto>());

            var controller = new UsersController(
                mockGetAll.Object,
                Mock.Of<IAdd<UserDto>>(),
                Mock.Of<IRemove>(),
                Mock.Of<IGetByName<UserDto>>(),
                Mock.Of<IGetById<UserDto>>(),
                Mock.Of<IUpdate<UserDto>>(),
                Mock.Of<IGetByEmail<UserDto>>()
            );

            // Act  
            var result = controller.GetAll();

            // Assert  
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
        }

        [Fact]
        public void AddUser_ReturnsOkResult()
        {
            // Arrange  
            var mockAdd = new Mock<IAdd<UserDto>>();
            mockAdd.Setup(s => s.Execute(userOk)).Returns(1);

            var controller = new UsersController(
                Mock.Of<IGetAll<UserDto>>(),
                mockAdd.Object,
                Mock.Of<IRemove>(),
                Mock.Of<IGetByName<UserDto>>(),
                Mock.Of<IGetById<UserDto>>(),
                Mock.Of<IUpdate<UserDto>>(),
                Mock.Of<IGetByEmail<UserDto>>()
            );

            // Act  
            var result = controller.Create(userOk);

            // Assert  
            var okResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(userOk, okResult.Value);
        }

        [Fact]
        public void AddUser_ReturnsErrorResult()
        {
            // Arrange  
            var mockAdd = new Mock<IAdd<UserDto>>();
            mockAdd.Setup(s => s.Execute(userRepetido)).Throws(new System.Exception("Usuario repetido"));

            var controller = new UsersController(
                Mock.Of<IGetAll<UserDto>>(),
                mockAdd.Object,
                Mock.Of<IRemove>(),
                Mock.Of<IGetByName<UserDto>>(),
                Mock.Of<IGetById<UserDto>>(),
                Mock.Of<IUpdate<UserDto>>(),
                Mock.Of<IGetByEmail<UserDto>>()
            );

            // Act  
            var result = controller.Create(userRepetido);

            // Assert  
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Usuario repetido", badRequestResult.Value.ToString());
        }

        [Fact]
        public void AddUser_ReturnsErrorCILengthResult()
        {
            // Arrange  
            var mockAdd = new Mock<IAdd<UserDto>>();
            mockAdd.Setup(s => s.Execute(userCIErrorLength)).Throws(new System.Exception("CI debe de tener 8 caracteres.")); //origen en User.cs

            var controller = new UsersController(
                Mock.Of<IGetAll<UserDto>>(),
                mockAdd.Object,
                Mock.Of<IRemove>(),
                Mock.Of<IGetByName<UserDto>>(),
                Mock.Of<IGetById<UserDto>>(),
                Mock.Of<IUpdate<UserDto>>(),
                Mock.Of<IGetByEmail<UserDto>>()
            );

            // Act  
            var result = controller.Create(userCIErrorLength);

            // Assert  
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("CI debe de tener 8 caracteres.", badRequestResult.Value.ToString());
        }

        [Fact]
        public void AddUser_ReturnsErrorCINumberResult()
        {
            // Arrange  
            var mockAdd = new Mock<IAdd<UserDto>>();
            mockAdd.Setup(s => s.Execute(userCIErrorNum)).Throws(new System.Exception("CI debe de escribirse solo con números, incluyendo dígito verificador, sin guión ni puntos.")); //origen en User.cs

            var controller = new UsersController(
                Mock.Of<IGetAll<UserDto>>(),
                mockAdd.Object,
                Mock.Of<IRemove>(),
                Mock.Of<IGetByName<UserDto>>(),
                Mock.Of<IGetById<UserDto>>(),
                Mock.Of<IUpdate<UserDto>>(),
                Mock.Of<IGetByEmail<UserDto>>()
            );

            // Act  
            var result = controller.Create(userCIErrorNum);

            // Assert  
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("CI debe de escribirse solo con números, incluyendo dígito verificador, sin guión ni puntos.", badRequestResult.Value.ToString());
        }

        [Fact]
        public void AddUser_ReturnsErrorEmailResult()
        {
            // Arrange  
            var mockAdd = new Mock<IAdd<UserDto>>();
            mockAdd.Setup(s => s.Execute(userEmailError)).Throws(new System.Exception("Email inválido")); //origen en VO de Email

            var controller = new UsersController(
                Mock.Of<IGetAll<UserDto>>(),
                mockAdd.Object,
                Mock.Of<IRemove>(),
                Mock.Of<IGetByName<UserDto>>(),
                Mock.Of<IGetById<UserDto>>(),
                Mock.Of<IUpdate<UserDto>>(),
                Mock.Of<IGetByEmail<UserDto>>()
            );

            // Act  
            var result = controller.Create(userEmailError);

            // Assert  
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Email inválido", badRequestResult.Value.ToString());
        }

        [Fact]
        public void AddUser_ReturnsErrorPassResult()
        {
            // Arrange  
            var mockAdd = new Mock<IAdd<UserDto>>();
            mockAdd.Setup(s => s.Execute(userPassError)).Throws(new System.Exception("La contraseña debe tener al menos 8 caracteres, un número y una letra mayúscula.")); //origen en VO de Password

            var controller = new UsersController(
                Mock.Of<IGetAll<UserDto>>(),
                mockAdd.Object,
                Mock.Of<IRemove>(),
                Mock.Of<IGetByName<UserDto>>(),
                Mock.Of<IGetById<UserDto>>(),
                Mock.Of<IUpdate<UserDto>>(),
                Mock.Of<IGetByEmail<UserDto>>()
            );

            // Act  
            var result = controller.Create(userPassError);

            // Assert  
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("La contraseña debe tener al menos 8 caracteres, un número y una letra mayúscula.", badRequestResult.Value.ToString());
        }

        [Fact]
        public void AddUser_ReturnsErrorPhoneResult()
        {
            // Arrange  
            var mockAdd = new Mock<IAdd<UserDto>>();
            mockAdd.Setup(s => s.Execute(userPhoneError)).Throws(new System.Exception("El número de teléfono deben ser solo números.")); //origen en User.cs

            var controller = new UsersController(
                Mock.Of<IGetAll<UserDto>>(),
                mockAdd.Object,
                Mock.Of<IRemove>(),
                Mock.Of<IGetByName<UserDto>>(),
                Mock.Of<IGetById<UserDto>>(),
                Mock.Of<IUpdate<UserDto>>(),
                Mock.Of<IGetByEmail<UserDto>>()
            );

            // Act  
            var result = controller.Create(userPhoneError);

            // Assert  
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("El número de teléfono deben ser solo números.", badRequestResult.Value.ToString());
        }

        [Fact]
        public void AddUser_ReturnsErrorRoleResult()
        {
            // Arrange  
            var mockAdd = new Mock<IAdd<UserDto>>();
            mockAdd.Setup(s => s.Execute(userRoleError)).Throws(new System.Exception("Rol de usuario no válido")); //origen en User.cs

            var controller = new UsersController(
                Mock.Of<IGetAll<UserDto>>(),
                mockAdd.Object,
                Mock.Of<IRemove>(),
                Mock.Of<IGetByName<UserDto>>(),
                Mock.Of<IGetById<UserDto>>(),
                Mock.Of<IUpdate<UserDto>>(),
                Mock.Of<IGetByEmail<UserDto>>()
            );

            // Act  
            var result = controller.Create(userRoleError);

            // Assert  
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Rol de usuario no válido", badRequestResult.Value.ToString());
        }
    }
}
