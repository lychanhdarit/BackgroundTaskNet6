using BackgroundTaskNet6.Jobs;
using Hangfire;
using Hangfire.MemoryStorage;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings() 
        .UseMemoryStorage()
        //.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
        //{
        //    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        //    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        //    QueuePollInterval = TimeSpan.Zero,
        //    UseRecommendedIsolationLevel = true,
        //    DisableGlobalLocks = true
        //})
        );

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();


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

app.UseHangfireDashboard();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");


//    endpoints.MapAreaControllerRoute(
//         name: "Admin",
//         areaName: "AdminCP",
//         pattern: "admincp/{controller=Home}/{action=Index}/{id?}");

//    endpoints.MapAreaControllerRoute(
//        name: "Api",
//        areaName: "Api",
//        pattern: "api/{controller=Home}/{action=Index}/{id?}");

//    endpoints.MapAreaControllerRoute(
//        name: "en",
//        areaName: "en",
//        pattern: "en/{controller=Home}/{action=Index}/{id?}");

//    endpoints.MapRazorPages();
//    //endpoints.MapHangfireDashboard();
//});


//var backgroundJobs = new BackgroundJobClient(); 
//backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!")); 
//BackgroundJob.Schedule(() => Console.WriteLine("Schedule..."), TimeSpan.FromSeconds(5));
Console.Clear();
var recurringJob = new RecurringJobManager();
recurringJob.AddOrUpdate("Run Recurring Job",()=> DemoJobs.Print(), "* * * * *");
recurringJob.AddOrUpdate("Run Recurring Job 2", () => DemoJobs.Print2(), "0,2,5,7,10 * * * *");
app.Run();
