namespace SharedUseCase.InterfacesUC
{
    public interface IGetById <T>
    {
        T Execute(int id);
    }
}
