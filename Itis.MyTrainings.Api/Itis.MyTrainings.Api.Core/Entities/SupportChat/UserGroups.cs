namespace Itis.MyTrainings.Api.Core.Entities.SupportChat;

public class UserGroups : EntityBase
{
    public string Email { get; set; }
    public Group Group { get; set; }
}