using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces;
using BusinessLogic.RepositoriesInterfaces.UserInterface;
using Infrastructure.DataAccess.EF.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.EF
{
    public class UserRepo : IRepoUser
    {
        private SargaContext _context;

        public UserRepo(SargaContext context)
        {
            _context = context;
        }

        public int Add(User obj)
        {
            if (obj == null)
            {
                throw new BadRequestException("Esta vacio");
            }
            obj.Validar();
            switch (obj.Rol) 
            {
                case "Client":
                    obj = new Client(obj.Id, obj.Ci, obj.Name, obj.Password, obj.Email, obj.Phone, "Client");
                    _context.Clients.Add((Client)obj);
                    break;
                case "Seller":
                    obj = new Seller(obj.Id, obj.Ci, obj.Name, obj.Password, obj.Email, obj.Phone, "Seller");
                    _context.Sellers.Add((Seller)obj);
                    break;
                case "Administrator":
                    obj = new Administrator(obj.Id, obj.Ci, obj.Name, obj.Password, obj.Email, obj.Phone, "Administrator");
                    _context.Administrators.Add((Administrator)obj);
                    break;
                default:
                    throw new BadRequestException("Rol de usuario no válido");
            }
            _context.SaveChanges();
            return obj.Id;
        }

        public void Delete(int id)
        {
            User unU = GetById(id);
            switch (unU.Rol)
            {
                case "Client":
                    var client = _context.Clients.FirstOrDefault(c => c.Id == id);
                    if (client != null)
                    {
                        _context.Clients.Remove(client);
                    }
                    break;
                case "Seller":
                    var seller = _context.Sellers.FirstOrDefault(s => s.Id == id);
                    if (seller != null)
                    {
                        _context.Sellers.Remove(seller);
                    }
                    break;
                case "Administrator":
                    var admin = _context.Administrators.FirstOrDefault(a => a.Id == id);
                    if (admin != null)
                    {
                        _context.Administrators.Remove(admin);
                    }
                    break;
                default:
                    throw new BadRequestException("Rol de usuario no válido");
            }
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                var clients = _context.Clients.ToList<User>();
                var sellers = _context.Sellers.ToList<User>();
                var administrators = _context.Administrators.ToList<User>();

                return clients.Concat(sellers).Concat(administrators).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de usuarios", ex);
            }
        }

        public User GetByCi(string value)
        {
            // Buscar primero en Clients
            var client = _context.Clients.FirstOrDefault(usuario => usuario.Ci == value);
            if (client != null)
            {
                return client;
            }

            // Buscar en Sellers
            var seller = _context.Sellers.FirstOrDefault(usuario => usuario.Ci == value);
            if (seller != null)
            {
                return seller;
            }

            // Buscar en Administrators
            var administrator = _context.Administrators.FirstOrDefault(usuario => usuario.Ci == value);
            if (administrator != null)
            {
                return administrator;
            }

            // Si no se encuentra, retornar null
            return null;
        }

        public User GetByEmail(string value)
        {
            // Buscar primero en Clients
            var client = _context.Clients.FirstOrDefault(usuario => usuario.Email.Value == value);
            if (client != null)
            {
                return client;
            }

            // Buscar en Sellers
            var seller = _context.Sellers.FirstOrDefault(usuario => usuario.Email.Value == value);
            if (seller != null)
            {
                return seller;
            }

            // Buscar en Administrators
            var administrator = _context.Administrators.FirstOrDefault(usuario => usuario.Email.Value == value);
            if (administrator != null)
            {
                return administrator;
            }

            // Si no se encuentra, retornar null
            return null;
        }

        public User GetById(int id)
        {
            // Buscar primero en Clients
            var client = _context.Clients.FirstOrDefault(usuario => usuario.Id == id);
            if (client != null)
            {
                return client;
            }

            // Buscar en Sellers
            var seller = _context.Sellers.FirstOrDefault(usuario => usuario.Id == id);
            if (seller != null)
            {
                return seller;
            }

            // Buscar en Administrators
            var administrator = _context.Administrators.FirstOrDefault(usuario => usuario.Id == id);
            if (administrator != null)
            {
                return administrator;
            }

            // Si no se encuentra, retornar null
            return null;
        }

        public IEnumerable<User> GetByName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new BadRequestException("El valor de búsqueda no puede estar vacío");
            }

            var lowerValue = value.ToLower();

            var clients = _context.Clients
                .Where(usuario => usuario.Name.Value.ToLower().Contains(lowerValue))
                .ToList<User>();

            var sellers = _context.Sellers
                .Where(usuario => usuario.Name.Value.ToLower().Contains(lowerValue))
                .ToList<User>();

            var administrators = _context.Administrators
                .Where(usuario => usuario.Name.Value.ToLower().Contains(lowerValue))
                .ToList<User>();

            return clients.Concat(sellers).Concat(administrators).ToList();
        }

        public User GetByName(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, User obj)
        {
            if (obj == null)
            {
                throw new BadRequestException("Está vacío");
            }
            obj.Validar();
            User unU = GetById(id);

            // Actualizar los campos del usuario
            unU.Update(obj);

            // Actualizar en la tabla correspondiente según el rol
            switch (unU.Rol)
            {
                case "Client":
                    var client = _context.Clients.FirstOrDefault(c => c.Id == id);
                    if (client == null)
                        throw new NotFoundException("No se encontró el cliente para actualizar");
                    client.Update(unU);
                    _context.Clients.Update(client);
                    break;
                case "Seller":
                    var seller = _context.Sellers.FirstOrDefault(s => s.Id == id);
                    if (seller == null)
                        throw new NotFoundException("No se encontró el vendedor para actualizar");
                    seller.Update(unU);
                    _context.Sellers.Update(seller);
                    break;
                case "Administrator":
                    var admin = _context.Administrators.FirstOrDefault(a => a.Id == id);
                    if (admin == null)
                        throw new NotFoundException("No se encontró el administrador para actualizar");
                    admin.Update(unU);
                    _context.Administrators.Update(admin);
                    break;
                default:
                    throw new BadRequestException("Rol de usuario no válido");
            }

            _context.SaveChanges();
        }

        
        User IRepoGetByEmail<User>.GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
