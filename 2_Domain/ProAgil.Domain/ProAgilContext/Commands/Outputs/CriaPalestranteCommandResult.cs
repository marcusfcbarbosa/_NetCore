using ProAgil.Shared.Commands;

namespace ProAgil.Domain.ProAgilContext.Commands.Outputs
{
    public class CriaPalestranteCommandResult : ICommandResult
    {
        public CriaPalestranteCommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success {  get;  set;}
        public string Message {  get;  set; }
        public object Data {  get;  set; }
    }
}