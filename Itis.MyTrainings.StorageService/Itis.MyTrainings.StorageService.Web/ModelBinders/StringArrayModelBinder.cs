using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Itis.MyTrainings.StorageService.ModelBinders;

public class StringArrayModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (!bindingContext.HttpContext.Request.Query.TryGetValue(bindingContext.FieldName, out var values))
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        // Parse nested array structure
        var result = new List<string[]>();
        foreach (var key in bindingContext.HttpContext.Request.Query.Keys)
        {
            if (!key.StartsWith("files[")) continue;
            var segments = key.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length != 2 || !int.TryParse(segments[1], out var i)) continue;
            while (result.Count <= i) result.Add(new List<string>().ToArray());
            result[i] = result[i].Concat(new string[] { bindingContext.HttpContext.Request.Query[key] }).ToArray();
        }

        bindingContext.Result = ModelBindingResult.Success(result.ToArray());
        return Task.CompletedTask;
    }
}