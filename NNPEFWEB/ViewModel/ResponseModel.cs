

namespace NNPEFWEB.ViewModel
{
    public class ResponseModel<T> where T : class
    {
        public ResponseModel()
        {
            Success = false;
            Message = "Something went wrong";
            Data = null;
        }

        public ResponseModel(string message , bool success)
        {
            Success = success;
            Message = message;
            Data = null;
        }
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
