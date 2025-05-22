using AppLogic.Exceptions.RepoException;
using BusinessLogic.RepositoriesInterfaces;
using SharedUseCase.InterfacesUC;

namespace AppLogic.UseCase.User
{
    public class DeleteUser: IRemove
    {
        private IRepoUser _repo;

        public DeleteUser(IRepoUser repo)
        {
            _repo = repo;
        }
        public void Execute(int id)
        {
            try
            {
                _repo.Delete(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException("Error al eliminar el usuario", ex);
            }

        }
    }
}
