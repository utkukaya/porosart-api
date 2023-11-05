namespace porosartapi.model.ViewModel;


public class ResponseVM
{
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string StatusCode { get; set; }
        public ResponseVM() { }
        public ResponseVM(bool isSuccess) { IsSuccess = isSuccess; }
        public ResponseVM(string errorMessage) { Message = errorMessage; IsSuccess = false; }
        public ResponseVM(bool isSuccess, string message) { IsSuccess = isSuccess; Message = message; }
}
public class ResponseVM<T> where T : class
{
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string StatusCode { get; set; }
        public ResponseVM() { }
        public ResponseVM(string errorMessage) { Message = errorMessage; IsSuccess = false; }
        public ResponseVM(bool isSuccess, string message) { IsSuccess = isSuccess; Message = message; }
        public ResponseVM(T data) { IsSuccess = true; Data = data; }
}
