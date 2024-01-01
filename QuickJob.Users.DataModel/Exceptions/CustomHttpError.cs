namespace QuickJob.Users.DataModel.Exceptions;

public sealed record CustomHttpError(string? Code, string? Message = null)
{
    
}
