using Lab4;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        var applicationLifetime = app.Services
        .GetRequiredService<IHostApplicationLifetime>();
        applicationLifetime.ApplicationStopping.Register(OnShutdown);
        app.Run();
    }
    //Метод для обробки зупинки застосунку
    public static void OnShutdown()
    {
        NHibernateDAOFactory.getInstance().destroy();
    }
}