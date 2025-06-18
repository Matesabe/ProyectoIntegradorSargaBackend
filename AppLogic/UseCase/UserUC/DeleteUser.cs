using AppLogic.Exceptions.RepoException;
using BusinessLogic.RepositoriesInterfaces;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;

namespace AppLogic.UseCase.User
{
    public class DeleteUser: IRemove<UserDto>
    {
        private IRepoUser _repo;
        public DeleteUser(IRepoUser repo)
        {
            _repo = repo;
        }
        public void Execute(UserDto user)
        {
            try
            {
                int id = user.Id;
                _repo.Delete(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException("Error al eliminar el usuario", ex);
            }
        }
    }
}
