namespace ApiLinkedIn.Models
{
    public class ApiResponse
    {
        #region Propiedades
        public bool IsSuccess { get; set; }
        public string? Messege { get; set; }
        public object? Result { get; set; }
        #endregion
    }
}
