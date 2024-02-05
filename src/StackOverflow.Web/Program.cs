using Autofac;
using Autofac.Extensions.DependencyInjection;
using log4net;
using StackOverflow.BL;
using StackOverflow.DAL;
using StackOverflow.DAL.Extensions;
using StackOverflow.Web;

var builder = WebApplication.CreateBuilder(args);

// Add Log4Net
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();
var log = LogManager.GetLogger(typeof(Program));

try
{
    log.Info("Application is starting...");
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("Connection String Not found");

    // Add NHibernate
    builder.Services.AddNHibernate(connectionString);
    
    // Add Identity
    builder.Services.AddIdentity();

    //Add Autofac Configuration
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
    {
        cb.RegisterModule(new WebModule());
        cb.RegisterModule(new DALModule());
        cb.RegisterModule(new BLModule());
    });

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    await app.Services.GetRequiredService<ISeederService>().Seed();
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );
    });

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch(Exception ex)
{
    log.Error(ex.Message, ex);
}