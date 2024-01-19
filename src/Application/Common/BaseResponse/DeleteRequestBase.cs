using MediatR;

namespace BrazilGeographicalData.src.Application.Common.BaseResponse
{
    public abstract class DeleteRequestBase<TResponse> : IRequest<TResponse>
    {
        public Guid Id { get; }

        protected DeleteRequestBase(Guid id)
        {
            Id = id;
        }
    }
}
