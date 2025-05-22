using AppLogic.Exceptions.RepoException;
using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;

namespace AppLogic.UseCase.User
{
    public class GetByIdUser: IGetById<UserDto>
    {
        private IRepoUser _repo;

        public GetByIdUser(IRepoUser repo)
        {
            _repo = repo;
        }

        public UserDto Execute(int id)
        {
            return UserMapper.ToDto(_repo.GetById(id));
        }

        UserDto IGetById<UserDto>.Execute(int id)
        {
            try
            {
                return UserMapper.ToDto(_repo.GetById(id));
            }
            catch (Exception ex)
            {
                throw new GetByIdException("Error al obtener el usuario por ID", ex);
            }
        }
    }
}
