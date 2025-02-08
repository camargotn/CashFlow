namespace CashFlow.Exception.ExceptionBase;
public class NotFoundException : CashFlowException
{
    public List<string> Errors { get; set; }

    public NotFoundException(string message) : base(message)
    {
    }
}
