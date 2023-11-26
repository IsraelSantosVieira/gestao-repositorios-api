namespace RepositorioApp.Utilities.Results
{
    public class EnvelopFailDataResult<T> : EnvelopResult
    {
        public EnvelopFailDataResult()
        {
            Success = false;
        }

        public T Data { get; set; }

        public static EnvelopDataResult<T> Fail(T data)
        {
            return new EnvelopDataResult<T>
            {
                Success = false,
                Data = data
            };
        }
    }
}
