
namespace Libreria.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioGetById <T>
    {
        T GetById(int id);
    }
}
