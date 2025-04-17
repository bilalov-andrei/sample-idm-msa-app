using MediatR;

namespace IDM.Common.Application.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}
