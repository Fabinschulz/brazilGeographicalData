namespace BrazilGeographicalData.src.Application.Common.BaseResponse
{
    public abstract class DeleteResponseBase
    {
        public bool IsSuccess { get; }

        protected DeleteResponseBase(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
