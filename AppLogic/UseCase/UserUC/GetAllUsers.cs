
using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;

namespace AppLogic.UseCase.User
{
    public class GetAllUsers: IGetAll<UserDto>
    {
        private IRepoUser _repo;

        public GetAllUsers(IRepoUser repo)
        {
            _repo = repo;
        }

        public IEnumerable<UserDto> Execute()
        {
            try
            {
                return UserMapper.ToListDto(_repo.GetAll());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los usuarios", ex);
            }
        }
    }
}
