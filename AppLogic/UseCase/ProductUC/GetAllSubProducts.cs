
using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces.SubProductInterface;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;

namespace AppLogic.UseCase.User
{
    public class GetAllSubProducts: IGetAll<SubProductDto>
    {
        private IRepoSubProduct _repo;

        public GetAllSubProducts(IRepoSubProduct repo)
        {
            _repo = repo;
        }

        public IEnumerable<SubProductDto> Execute()
        {
            try
            {
                return SubproductMapper.ToListDto(_repo.GetAll());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los sub productos", ex);
            }
        }
    }
}
