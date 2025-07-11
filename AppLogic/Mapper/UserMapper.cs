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
            return new UserDto(usuario.Id,
                                usuario.Ci,
                                usuario.Name.Value,
                               usuario.Email.Value,
                               usuario.Password.Value,
                               usuario.Phone,
                               usuario.Rol);
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
