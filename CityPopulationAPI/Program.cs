using AutoMapper;

using CityPopulationAPI.Mapper;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();

        // Register AutoMapper
        builder.Services.AddAutoMapper(typeof(Program), typeof(MappingProfile));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "City API V1");
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
