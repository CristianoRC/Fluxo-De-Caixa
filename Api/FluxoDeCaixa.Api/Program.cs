using FluxoDeCaixa.Api;
using FluxoDeCaixa.Domain;
using FluxoDeCaixa.Infra;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new XssSanitizeJsonConvert()));

builder.Host.UseSerilog((builderContext, _, configuration) =>
{
    if (builderContext.HostingEnvironment.IsDevelopment())
        configuration.WriteTo.Console(theme: AnsiConsoleTheme.Literate);
    else
        configuration.WriteTo.File("./logs.txt");
    //em um ambiente real o ideal seria gravar em um serviço externo
});

const string corsConfig = "_personalDomainCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsConfig,
        policy =>
        {
            //Em um ambiente real precisaríamos configurar uma para dev e uma para prod com as origens corretas
            policy
                .WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddDomain().AddInfra();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();