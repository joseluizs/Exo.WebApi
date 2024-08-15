using Exo.WebApi.Contexts;
using Exo.WebApi.Repositories;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExoContext, ExoContext>();
builder.Services.AddControllers();

//Forma de autenticação
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
//Parametros de validação do token
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //valida quen esta solicintando
        ValidateIssuerSigningKey = true,
        //validaque quem esta recebendo
        ValidateAudience = true,
        //define o tempo de expiração
        ValidateLifetime = true,
        //criptografia a validação
        IssuerSigningKey = new
            SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticação")),
        //valida o tempo de expiração
        ClockSkew = TimeSpan.FromMinutes(30),
        //nome do issuer
        ValidIssuer = "exoapi.webapi",
        //nome do audience p/destino
        ValidAudience = "exoapi.webapi"
    };

});


builder.Services.AddTransient<ProjetoRepository, ProjetoRepository>();
builder.Services.AddTransient<UsuarioRepository, UsuarioRepository>();

var app = builder.Build();

app.UseRouting();

//habilita a autenticação
app.UseAuthentication();
//habilita a autorização
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
