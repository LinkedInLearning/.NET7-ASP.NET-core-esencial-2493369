namespace demoinyeccion.Helpers;

public static class DemoInyeccionExtensionsHelper
{
    public static void AddCurso(this IServiceCollection services)
    {
        services.AddScoped<IGuidinator, Guidinator>();
    }
}
