using System.Reflection;

namespace Itis.MyTrainings.StorageService.Core.Helpers;

public static class AssemblyHelper
{
    public static readonly Assembly CoreAssembly = typeof(AssemblyHelper).Assembly;
}