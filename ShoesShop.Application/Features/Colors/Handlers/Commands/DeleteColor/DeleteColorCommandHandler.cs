using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Colors.Requests.Commands;

namespace ShoesShop.Application.Features.Colors.Handlers.Commands.DeleteColor
{
    public sealed class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, bool>
    {
        private readonly IColorRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteColorCommandHandler(IColorRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
        {
            var color = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (color is null || color.IsDeleted)
            {
                return false;
            }

            color.IsDeleted = true;

            color.LastUpdateDate = DateTime.Now;

            _repository.Update(color);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
