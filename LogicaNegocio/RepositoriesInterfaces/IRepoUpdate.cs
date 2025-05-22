
namespace Libreria.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepoUpdate <T>
    {
        void Update(int id, T obj);
    }
}
