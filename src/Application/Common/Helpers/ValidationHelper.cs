using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ValidationException = CopperConsumption.Application.Common.Exceptions.ValidationException;

namespace CopperConsumption.Application.Common.Helpers
{
    public static class ValidationHelper
    {
        public static async Task Validate<T>(AbstractValidator<T> validator, T entity)
        {
            var validationResult = await validator.ValidateAsync(entity);

            var failures = validationResult.Errors
                                           .Where(f => f != null)
                                           .ToList();

            if (failures.Count != 0)
                    throw new ValidationException(failures);
        }
    }
}