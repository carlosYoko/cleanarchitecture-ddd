namespace CleanArchitecture.Application.Exceptions
{
    public class NotFoundExcpetion : ApplicationException
    {
        public NotFoundExcpetion(string name, object key) : base($"Entity \"{name}\" ({key}) no se ha encontrado")
        {
        }
    }
}
