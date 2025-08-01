using BusinessLogic.Entities;
using BusinessLogic.VO;
using SharedUseCase.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppLogic.Mapper
{
    public static class UserMapper
    {
        public static User FromDto(UserDto dto)
        {
            return dto.Rol switch
            {
                "Client" => FromDtoClient(dto),
                "Seller" => FromDtoSeller(dto),
                "Administrator" => FromDtoAdministrator(dto),
                _ => throw new ArgumentException("Rol de usuario no válido")
            };
        }
        public static User FromDtoClient(UserDto userDto)
        {
            return new Client(userDto.Id,
                              userDto.Ci, // Fixed: Accessing the 'Ci' property from the 'userDto' instance
                              new Name(userDto.Name),
                              new Password(userDto.Password),
                              new Email(userDto.Email),
                              userDto.Phone, // Fixed: Corrected casing to match the property name
                              "Client");
        }

        public static User FromDtoSeller(UserDto userDto)
        {
            return new Seller(userDto.Id,
                              userDto.Ci, // Fixed: Accessing the 'Ci' property from the 'userDto' instance
                              new Name(userDto.Name),
                              new Password(userDto.Password),
                              new Email(userDto.Email),
                              userDto.Phone, // Fixed: Corrected casing to match the property name
                              "Seller");
        }

        public static User FromDtoAdministrator(UserDto userDto)
        {
            return new Administrator(userDto.Id,
                              userDto.Ci, // Fixed: Accessing the 'Ci' property from the 'userDto' instance
                              new Name(userDto.Name),
                              new Password(userDto.Password),
                              new Email(userDto.Email),
                              userDto.Phone, // Fixed: Corrected casing to match the property name
                              "Administrator");
        }

        public static UserDto ToDto(User usuario)
        {
            return usuario.Rol switch // Fixed: Changed 'dto' to 'usuario' to match the parameter name
            {
                "Client" => ToDtoClient(usuario), // Fixed: Changed 'dto' to 'usuario'
                "Seller" => ToDtoNormal(usuario), // Fixed: Added missing case for "Seller"
                "Administrator" => ToDtoNormal(usuario), // Fixed: Added missing case for "Administrator"
                _ => throw new ArgumentException("Rol de usuario no válido") // Fixed: Replaced 'default' with '_'
            };
        }

        public static UserDto ToDtoNormal(User user)
        {
            return new UserDto(
                user.Id,
                user.Ci,
                user.Name.ToString(),
                user.Email.ToString(),
                user.Password.ToString(),
                user.Phone,
                user.Rol,
                0, // Asume que Points es 0 para usuarios no clientes
                0
            );
        }

        public static UserDto ToDtoClient(User user)
        {
            var client = user as Client;
            return new UserDto(
                user.Id,
                user.Ci,
                user.Name.ToString(),
                user.Email.ToString(),
                user.Password.ToString(),
                user.Phone,
                "Client",
                client != null ? client.Points : 0, // Asume que Points es una propiedad de Client
                client != null ? client.RecurrenceCount : 0 // Asume que RecurrenceCount es una propiedad de Client
            );
        }


        public static IEnumerable<UserDto> ToListDto(IEnumerable<User> usuarios)
        {
            List<UserDto> usuariosDto = new List<UserDto>();
            foreach (var item in usuarios)
            {
                usuariosDto.Add(ToDto(item));
            }
            return usuariosDto;
        }
    }
}
