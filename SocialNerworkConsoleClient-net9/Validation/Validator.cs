using System.ComponentModel.DataAnnotations;

namespace SocialNerworkConsoleClient_net9.Commands;

public static class Validator
{
    public static string ValidateModel<TModel>(TModel model)
    {
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(model, context, results, true);

        if (!isValid)
        {
            var message = string.Join(Environment.NewLine, results.Select(x => x.ErrorMessage));
            throw new Exception(message);
        }

        return null;
    }
}