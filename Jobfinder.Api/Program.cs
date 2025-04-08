
using Amazon.S3;
using Jobfinder.Api.Extensions;
using Jobfinder.Application.Extensions;
using Jobfinder.Infrastructure.Extensions;
using Jobfinder.Infrastructure.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
var app = builder.Build();

using (var scoped = app.Services.CreateScope())
{
    var s3 = scoped.ServiceProvider.GetRequiredService<IAmazonS3>();
    var seed = new S3Seed(s3);
    var result = await seed.CheckBucketAndCreate("job-seeker");
    if (!result)
        throw new Exception("S3 Bucket problem");
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddEndpoints();
app.Run();

