namespace ContosoPizza.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"{name}, com o id '{key}' não encontrado.")
}