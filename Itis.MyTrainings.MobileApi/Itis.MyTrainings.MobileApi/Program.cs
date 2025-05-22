using Itis.MyTrainings.MobileApi.Mutations;
using Itis.MyTrainings.MobileApi.Queries;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient("ApiClient"
    ,config=>config.BaseAddress = new Uri(builder.Configuration["Api:Url"]));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b =>
        {
            b.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
builder.Services.AddGraphQLServer()
    .AddQueryType<AuthQuery>()
    .AddMutationType<AuthMutations>();
var app = builder.Build();
app.UseCors("AllowAll");


app.MapGraphQL("/api/graphql");

app.Run();