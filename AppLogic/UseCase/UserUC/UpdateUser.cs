using AppLogic.Exceptions;
using AppLogic.Exceptions.RepoException;
using AppLogic.Mapper;
using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;

namespace AppLogic.UseCase.UserUC
{
    public class UpdateUser : IUpdate<UserDto>
    {
        private IRepoUser _repo;

        public UpdateUser(IRepoUser repo)
        {
            _repo = repo;
        }

        public void Execute(int id, UserDto obj)
        {
            try
            {
                BusinessLogic.Entities.User user = _repo.GetById(id); // Fully qualify 'User' to resolve ambiguity
                if (user == null) {
                    throw new NotFoundException("Usuario no encontrado");
                }
                switch
                    (obj.Rol)
                {
                    case "Client":
                        _repo.Update(id, Mapper.UserMapper.FromDtoClient(obj));
                        break;
                    case "Seller":
                        _repo.Update(id, Mapper.UserMapper.FromDtoSeller(obj));
                        break;
                    case "Administrator":
                        _repo.Update(id, Mapper.UserMapper.FromDtoAdministrator(obj));
                        break;
                    default:
                        throw new ArgumentException("Rol de usuario no válido");
                }
            }
            catch (Exception ex)
            {
                throw new UpdateException("Error al actualizar el usuario", ex);
            }
        }
    }
}
