using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Colors.Requests.Commands;

namespace ShoesShop.Application.Features.Colors.Handlers.Commands.UpdateColor
{
    public sealed class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommand, bool>
    {
        private readonly IColorRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateColorCommandHandler(IColorRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
        {
            var color = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (color == null || color.IsDeleted)
            {
                return false;
            }

            color.ColorName = request.ColorName.Trim();

            color.LastUpdateDate = DateTime.Now;

            _repository.Update(color);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
