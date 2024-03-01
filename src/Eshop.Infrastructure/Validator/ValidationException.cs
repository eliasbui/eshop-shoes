namespace Eshop.Infrastructure.Validator;

public class ValidationException(ValidationResultModel validationResultModel) : Exception
{
    public ValidationResultModel ValidationResultModel { get; } = validationResultModel;
}