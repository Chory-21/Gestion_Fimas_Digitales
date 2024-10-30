using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Gestion_Fimas_Digitales.Models;

public class JsonModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var jsonString = new System.IO.StreamReader(bindingContext.HttpContext.Request.Body).ReadToEnd();

        if (string.IsNullOrEmpty(jsonString))
        {
            return Task.CompletedTask;
        }

        var model = JsonConvert.DeserializeObject<FirmaDigital>(jsonString);
        bindingContext.Result = ModelBindingResult.Success(model);

        return Task.CompletedTask;
    }
}
