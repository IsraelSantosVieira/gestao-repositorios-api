namespace RepositorioApp.Utilities.Results
{
    public class EnvelopDataResult<T> : EnvelopResult
    {
        public EnvelopDataResult()
        {
            Success = true;
        }

        public T Data { get; set; }

        public static EnvelopDataResult<T> Ok(T data, bool success = true)
        {
            return new EnvelopDataResult<T>
            {
                Success = success,
                Data = data
            };
        }
    }
}
