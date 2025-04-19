using GAC_WMS_RestApi.DatabaseConfig;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Spi;
using Microsoft.Extensions.DependencyInjection;
using GAC_WMS_RestApi.PollingJob;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var cronExpression = builder.Configuration.GetValue<string>("QuartzConfig:CronExpression");

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
}); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//ajmal custom code
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Quartz services
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();  // DI for job creation

    // Define job and trigger
    var jobKey = new JobKey("SimpleJob");

    // Add job to Quartz container
    q.AddJob<FolderPollingJob>(opts => opts.WithIdentity(jobKey));

    // Add trigger to run the job based on the cron expression from the appsettings
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("SimpleJobTrigger")
        .WithCronSchedule(cronExpression)  // Use cron expression from appsettings.json
    );
});

// Add QuartzHostedService to run jobs in the background
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
