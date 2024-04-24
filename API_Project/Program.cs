using API_Project.Extensions;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace API_Project
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApiProjectContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddDbContext<IdentityContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("Identity")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.ApplicationService(builder.Configuration);
            builder.Services.AddIdentityService(builder.Configuration);

            var app = builder.Build();

            await DbInit.InitializeDbAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }


    }
}
