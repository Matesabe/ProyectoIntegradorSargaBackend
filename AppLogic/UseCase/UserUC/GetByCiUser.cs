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
    public class GetByCiUser : IGetByCi<UserDto>
    {
        private IRepoUser _repo;

        public GetByCiUser(IRepoUser repo)
        {
            _repo = repo;
        }

        // Fix: Ensure the method signature matches the interface definition
        public UserDto Execute(string ci)
        {
            try
            {
                var user = _repo.GetByCi(ci);
                if (user == null)
                    return null;

                return UserMapper.ToDto(user);
            }
            catch (Exception ex)
            {
                throw new GetByEmailException("Error al obtener el usuario por CI", ex);
            }
        }
    }
}
