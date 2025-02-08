using System.Net;

namespace CashFlow.Exception.ExceptionBase;
public class NotFoundException : CashFlowException
{
    public List<string> Errors { get; set; }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public NotFoundException(string message) : base(message)
    {
    }

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
