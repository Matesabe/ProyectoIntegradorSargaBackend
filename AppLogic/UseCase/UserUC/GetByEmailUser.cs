using AppLogic.Exceptions.RepoException;
using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.UserUC
{
    public class GetByEmailUser : IGetByEmail<UserDto>
    {
        private IRepoUser _repo;

        public GetByEmailUser(IRepoUser repo)
        {
            _repo = repo;
        }

        // Fix: Ensure the method signature matches the interface definition
        public UserDto Execute(string email)
        {
            try
            {
                var user = _repo.GetByEmail(email);
                if (user == null)
                    return null;

                return UserMapper.ToDto(user);
            }
            catch (Exception ex)
            {
                throw new GetByEmailException("Error al obtener el usuario por Email", ex);
            }
        }
    }
}
