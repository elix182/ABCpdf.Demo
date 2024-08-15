namespace Demo.Common.Models.Result
{
    public class ServiceResult
    {
        public OperationStatus Status { get; set; }
        public IList<string> Notes { get; set; }

        public bool IsError { get => Status.Result == OperationStatusResult.Error; }
        public bool IsSuccess { get => Status.Result == OperationStatusResult.Success; }
        public string Message { get => Status.Message; }

        public ServiceResult() 
        {
            Status = new OperationStatus()
            {
                Result = OperationStatusResult.Warning
            };
            Notes = new List<string>();
        }

        public static ServiceResult CreateSuccessResult()
        {
            return new ServiceResult()
            {
                Status = new OperationStatus()
                {
                    Result = OperationStatusResult.Success
                }
            };
        }

        public static ServiceResult CreateErrorResult()
        {
            return new ServiceResult()
            {
                Status = new OperationStatus()
                {
                    Result = OperationStatusResult.Error
                }
            };
        }

        public static ServiceResult CreateErrorResult(Exception ex)
        {
            return new ServiceResult()
            {
                Status = new OperationStatus()
                {
                    Result = OperationStatusResult.Error,
                    Message = ex.Message
                }
            };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public bool IsDataNull { get => Data == null; }

        public static ServiceResult<T> CreateSuccessResult(T data)
        {
            return new ServiceResult<T>()
            {
                Status = new OperationStatus()
                {
                    Result = OperationStatusResult.Success
                },
                Data = data
            };
        }

        public static new ServiceResult<T> CreateErrorResult()
        {
            return new ServiceResult<T>()
            {
                Status = new OperationStatus()
                {
                    Result = OperationStatusResult.Error
                }
            };
        }

        public static new ServiceResult<T> CreateErrorResult(Exception ex)
        {
            return new ServiceResult<T>()
            {
                Status = new OperationStatus()
                {
                    Result = OperationStatusResult.Error,
                    Message = ex.Message
                }
            };
        }
    }
}
