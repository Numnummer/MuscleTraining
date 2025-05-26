using Itis.MyTrainings.MobileApi.Model;

namespace Itis.MyTrainings.MobileApi.Queries;

public class Query:ObjectType
{
    [GraphQLDescription("Authentication related queries")]
    public AuthQuery Auth { get; } 

    [GraphQLDescription("Exercise management queries")]
    public ExerciseQuery Exercises { get; }

    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor.Field("GetAllExercises")
            .Type<ListType<ObjectType<Exercise>>>() 
            .Argument("jwtToken", a => a.Type<NonNullType<StringType>>()) 
            .Resolve(async context =>
            {
                var exerciseQuery = context.Service<ExerciseQuery>();

                var jwtToken = context.ArgumentValue<string>("jwtToken");

                var exercises = await exerciseQuery.GetAllExercises(jwtToken);

                return exercises;
            });
        
        descriptor.Field("Login")
            .Argument("email", a => a.Type<NonNullType<StringType>>())
            .Argument("password", a => a.Type<NonNullType<StringType>>())
            .Type<ObjectType<AuthResponse>>()
            .Resolve(async context =>
            {
                var authQuery = context.Service<AuthQuery>();

                var email = context.ArgumentValue<string>("email");
                var password = context.ArgumentValue<string>("password");

                var result = await authQuery.Login(email, password);

                return result;
            });
    }
}