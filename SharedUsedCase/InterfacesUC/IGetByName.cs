namespace SharedUseCase.InterfacesUC
{
    public interface IGetByName <T>
    {
        IEnumerable<T> Execute(string valor);
    }
}

