using AppLogic.Exceptions.RepoException;
using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;

namespace AppLogic.UseCase.User
{
    public class AddUser: IAdd<UserDto>
    {
        private IRepoUser _repo;
        public AddUser(IRepoUser repo)
        {
            _repo = repo;
        }

        public int Execute(UserDto obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
                }
                if (string.IsNullOrWhiteSpace(obj.Ci))
                {
                    throw new ArgumentException("El campo 'Ci' no puede estar vacío", nameof(obj.Ci));
                }
                if (string.IsNullOrWhiteSpace(obj.Name))
                {
                    throw new ArgumentException("El campo 'Name' no puede estar vacío", nameof(obj.Name));
                }
                if (string.IsNullOrWhiteSpace(obj.Email))
                {
                    throw new ArgumentException("El campo 'Email' no puede estar vacío", nameof(obj.Email));
                }
                if (string.IsNullOrWhiteSpace(obj.Password))
                {
                    throw new ArgumentException("El campo 'Password' no puede estar vacío", nameof(obj.Password));
                }
                if (string.IsNullOrWhiteSpace(obj.Rol))
                {
                    throw new ArgumentException("El campo 'Rol' no puede estar vacío", nameof(obj.Rol));
                }

                // Verificar duplicados por Email
                var userByEmail = _repo.GetByEmail(obj.Email);
                if (userByEmail != null)
                {
                    throw new ArgumentException("Ya existe un usuario con el mismo Email", nameof(obj.Email));
                }

                // Verificar duplicados por CI
                var userByCi = _repo.GetByCi(obj.Ci);
                if (userByCi != null)
                {
                    throw new ArgumentException("Ya existe un usuario con el mismo CI", nameof(obj.Ci));
                }

                // Verificar duplicados por Phone (si el campo Phone no es nulo o vacío)
                if (!string.IsNullOrWhiteSpace(obj.Phone))
                {
                    var usuarios = _repo.GetAll();
                    if (usuarios.Any(u => u.Phone == obj.Phone))
                    {
                        throw new ArgumentException("Ya existe un usuario con el mismo teléfono", nameof(obj.Phone));
                    }
                }

                switch (obj.Rol)
                {
                    case "Client":
                        return _repo.Add(UserMapper.FromDtoClient(obj));
                    case "Seller":
                        return _repo.Add(UserMapper.FromDtoSeller(obj));
                    case "Administrator":
                        return _repo.Add(UserMapper.FromDtoAdministrator(obj));
                    default:
                        throw new ArgumentException("Rol de usuario no válido");
                }
            }
            catch (Exception ex)
            {
                throw new AddException("Error al agregar el usuario", ex);
            }
        }
    }
}
