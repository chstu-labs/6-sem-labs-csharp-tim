using Lab4;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
var applicationLifetime = app.Services
.GetRequiredService<IHostApplicationLifetime>();
applicationLifetime.ApplicationStopping.Register(() =>
{
    NHibernateDAOFactory.getInstance().destroy();
});
app.MapControllerRoute(
name: "default",
pattern: "{controller=Order}/{action=GetAll}");
app.Run();