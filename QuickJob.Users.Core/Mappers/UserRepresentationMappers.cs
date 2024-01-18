using FS.Keycloak.RestApiClient.Model;
using QuickJob.Users.DataModel.Api.Requests.Registration;
using QuickJob.Users.DataModel.Constants;

namespace Users.Service.Mappers;

public static class UserRepresentationMappers
{
    public static UserRepresentation GenerateUserForm(this InitCreateUserRequest initCreateUserRequest)
    {
        return new UserRepresentation
        {
            Id = Guid.NewGuid().ToString(),
            Email = initCreateUserRequest.Email,
            Username = initCreateUserRequest.Email,
            EmailVerified = false,
            Enabled = false,
            FirstName = initCreateUserRequest.Fio,
            Attributes = new Dictionary<string, List<string>>
            {
                { KeycloackUserAttributes.Phone, new List<string> { initCreateUserRequest.Phone } },
                { KeycloackUserAttributes.Password, new List<string> { initCreateUserRequest.Password } },
                { KeycloackUserAttributes.Fio, new List<string> { initCreateUserRequest.Fio } }
            },
            Credentials = new List<CredentialRepresentation>
                { new() { Type = KeycloackUserAttributes.Password, Value = initCreateUserRequest.Password } }
        };
    }
    
    public static string? GetAttributeOrNull(this UserRepresentation user, string key) => 
        user.Attributes?.FirstOrDefault(x => x.Key == key).Value?.FirstOrDefault();
    
    public static void DeleteAttribute(this UserRepresentation user, string key) => 
        user.Attributes?.Remove(key);
}