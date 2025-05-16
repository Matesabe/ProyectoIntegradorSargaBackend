
namespace Libreria.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioGetAll <T>
    {
        IEnumerable<T> GetAll();
    }
}
