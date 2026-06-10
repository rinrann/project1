namespace ClinicMS.Models.DTOs
{
    public class ApiResponse<T>
    {
        public bool   Success { get; set; }
        public string Message { get; set; }
        public T      Data    { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "Success")
            => new ApiResponse<T> { Success = true, Message = message, Data = data };

        public static ApiResponse<T> Fail(string message)
            => new ApiResponse<T> { Success = false, Message = message };
    }

    public class ApiResponse : ApiResponse<object>
    {
        public static ApiResponse Ok(string message = "Success")
            => new ApiResponse { Success = true, Message = message };

        public new static ApiResponse Fail(string message)
            => new ApiResponse { Success = false, Message = message };
    }
}
