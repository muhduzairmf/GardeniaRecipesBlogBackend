using GardeniaRecipesBlogBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(o => {
    o.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }
    );
});
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
//    options => options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey= true,
//        IssuerSigningKey = new SymmetricSecurityKey(new System.Text.UTF8Encoding().GetBytes("Jx2VHbBSTbkPkrdOClH4rcYOdIqKdKpbyBDI45ELIwk7061c968eadfba2959d6e12d156590bafc")),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//    }
//);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "app")),
    RequestPath = "/app"
});

app.UseStaticFiles();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
