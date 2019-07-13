using ProAgil.Shared.Commands;

namespace ProAgil.Domain.ProAgilContext.Commands.Outputs
{
    public class DeletaEventoCommandResult : ICommandResult
    {
        public DeletaEventoCommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
       }

        public bool Success {  get;  set;}
        public string Message {  get;  set; }
        public object Data {  get;  set; }
    }
}