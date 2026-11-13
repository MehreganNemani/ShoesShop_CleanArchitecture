using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Features.Colors.Requests.Commands;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Features.Colors.Handlers.Commands.CreateColor
{
    public sealed class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, int>
    {
        private readonly IColorRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateColorCommandHandler(IColorRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateColorCommand request, CancellationToken cancellationToken)
        {
            var colorName = request.ColorName.Trim();

            var color = new Color
            {
                ColorName = colorName,
                CreateDate = DateTime.Now,
                IsDeleted = false
            };

            await _repository.AddAsync(color, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return color.ID;
        }
    }
}
