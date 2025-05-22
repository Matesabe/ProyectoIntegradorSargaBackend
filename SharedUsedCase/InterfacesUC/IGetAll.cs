
namespace SharedUseCase.InterfacesUC
{
    public interface IGetAll <T>
    {
        IEnumerable<T> Execute();
    }
}

