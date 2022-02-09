using BackgroundTaskNet6.Jobs;
using Hangfire;
using Hangfire.MemoryStorage;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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

app.UseAuthentication(); // Authentication - first
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangFireAuthorizationFilter() }
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
     

    endpoints.MapAreaControllerRoute(
        name: "Api",
        areaName: "Api",
        pattern: "api/{controller=Home}/{action=Index}/{id?}");
     

    endpoints.MapRazorPages();
    endpoints.MapHangfireDashboard();
});

//var dashboardOptions = new DashboardOptions { IgnoreAntiforgeryToken = true  }; 
//app.UseHangfireDashboard("/hangfire", dashboardOptions);

//var backgroundJobs = new BackgroundJobClient(); 
//backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!")); 
//BackgroundJob.Schedule(() => Console.WriteLine("Schedule..."), TimeSpan.FromSeconds(5));


var recurringJob = new RecurringJobManager();
//Cron S M H Dayofmonth Month Dayofweek Year

//Exe: run After 15 second
recurringJob.AddOrUpdate("Run Recurring Job",()=> DemoJobs.Print(), "0/15 * * * * *");
//Exe: Minute 0,5,10,15,30,45 Run
recurringJob.AddOrUpdate("Run Recurring Job 2", () => DemoJobs.Print2(), "0,5,10,15,30,47 * * * *");
app.Run();
