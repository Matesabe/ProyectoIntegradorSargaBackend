namespace SharedUseCase.InterfacesUC
{
    public interface IRemove<T>
    {
        void Execute(T item);
    }
}
