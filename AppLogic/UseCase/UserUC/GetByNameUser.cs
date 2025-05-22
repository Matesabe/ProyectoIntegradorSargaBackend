using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;

namespace AppLogic.UseCase.User
{
    public class GetByNameUser: IGetByName<UserDto>
    {
        private IRepoUser _repo;

        public GetByNameUser(IRepoUser repo)
        {
            _repo = repo;
        }

        public IEnumerable<UserDto> Execute(string valor)
        {
            try
            {
                return UserMapper.ToListDto(_repo.GetByName(valor));
            }
            catch
            {
                throw new Exception("Error al obtener usuarios por nombre");
            }
        }
    }
}
