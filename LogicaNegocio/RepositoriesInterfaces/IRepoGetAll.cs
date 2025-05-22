
namespace Libreria.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepoGetAll <T>
    {
        IEnumerable<T> GetAll();
    }
}
