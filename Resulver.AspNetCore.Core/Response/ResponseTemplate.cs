namespace Resulver.AspNetCore.Core.Response;

public class ResponseTemplate
{
    public string? Message { get; set; }
}

public class ResponseTemplate<TContent> : ResponseTemplate
{
    public TContent? Content { get; set; }
}