using log4net;
using StackOverflow.DAL.Extensions;

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

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch(Exception ex)
{
    log.Error(ex.Message, ex);
}