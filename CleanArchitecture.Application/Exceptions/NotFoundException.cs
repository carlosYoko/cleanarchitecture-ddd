namespace CleanArchitecture.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) no se ha encontrado")
        {
        }
    }
}
