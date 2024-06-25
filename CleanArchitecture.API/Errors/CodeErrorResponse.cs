namespace CleanArchitecture.API.Errors
{
    public class CodeErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public CodeErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statuscode)
        {
            return statuscode switch
            {
                400 => "El request tiene errores",
                401 => "No tienes autorización para este recurso",
                404 => "No se encontró el recurso",
                500 => "Error en el servidor",
                _ => string.Empty,
            };
        }
    }
}
