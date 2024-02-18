using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.GetUserProfileById;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Requests.UserProfile.GetUserProfileById;

/// <summary>
/// Обработчик запроса на получение профиля пользователя
/// </summary>
public class GetUserProfileByIdQueryHandler 
: IRequestHandler<GetUserProfileByIdQuery, GetUserProfileByIdResponse>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext"></param>
    public GetUserProfileByIdQueryHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc />
    public async Task<GetUserProfileByIdResponse> Handle(
        GetUserProfileByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.UserProfiles
            .FirstOrDefaultAsync(x => x.Id == request.Id, 
                cancellationToken)
            ?? throw new EntityNotFoundException<Entities.UserProfile>(request.Id);
        
        var response = new GetUserProfileByIdResponse()
        {
            Gender = entity.Gender,
            Height = entity.Height,
            DietaryPreferences = entity.DietaryPreferences,
            Weight = entity.Weight,
            PhoneNumber = entity.PhoneNumber,
            StartDate = entity.StartDate,
            MedicalSickness = entity.MedicalSickness,
            WeeklyTrainingFrequency = entity.WeeklyTrainingFrequency,
            DateOfBirth = entity.DateOfBirth
        };

        return response;
    }
}