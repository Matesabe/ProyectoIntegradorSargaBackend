namespace SharedUseCase.InterfacesUC
{
    public interface IUpdate <T>
    {
        void Execute(int id, T obj);
    }
}
