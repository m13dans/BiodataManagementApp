namespace PT_EDI_Indonesia_MVC.Service.ErrorService;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
