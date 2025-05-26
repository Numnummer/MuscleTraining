using Itis.MyTrainings.MobileApi.Model;

namespace Itis.MyTrainings.MobileApi.Mutations;

public class Mutations:ObjectType
{
    [GraphQLDescription("Authentication related mutations")]
    public AuthMutations Auth { get; } 

    [GraphQLDescription("Exercise management mutations")]
    public ExerciseMutations Exercises { get; }

    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor.Field("Register")
            .Argument("username", a => a.Type<NonNullType<StringType>>())
            .Argument("email", a => a.Type<NonNullType<StringType>>())
            .Argument("password", a => a.Type<NonNullType<StringType>>())
            .Type<ObjectType<AuthResponse>>()
            .Resolve(async context =>
            {
                var authMutations = context.Service<AuthMutations>();

                var username = context.ArgumentValue<string>("username");
                var email = context.ArgumentValue<string>("email");
                var password = context.ArgumentValue<string>("password");

                var result = await authMutations.Register(username, email, password);

                return result;
            });
        
        descriptor.Field("AddExercise")
            .Argument("jwtToken", a => a.Type<NonNullType<StringType>>())
            .Argument("name", a => a.Type<NonNullType<StringType>>())
            .Argument("description", a => a.Type<StringType>())
            .Type<BooleanType>()
            .Resolve(async context =>
            {
                var exerciseMutations = context.Service<ExerciseMutations>();

                var name = context.ArgumentValue<string>("name");
                var jwtToken = context.ArgumentValue<string>("jwtToken");
                var description = context.ArgumentValue<string>("description");

                await exerciseMutations.AddExercise(jwtToken, name, description);
                return true;
            });
        
        descriptor.Field("DeleteExercise")
            .Argument("jwtToken", a => a.Type<NonNullType<StringType>>())
            .Argument("id", a => a.Type<NonNullType<StringType>>())
            .Type<BooleanType>()
            .Resolve(async context =>
            {
                var exerciseMutations = context.Service<ExerciseMutations>();

                var jwtToken = context.ArgumentValue<string>("jwtToken");
                var id = context.ArgumentValue<string>("id");

                await exerciseMutations.DeleteExercise(jwtToken, id);
                return true;
            });
    }
}