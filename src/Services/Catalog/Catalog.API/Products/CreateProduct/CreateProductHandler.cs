namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, decimal Price, string ImageFile)
    : ICommand<CreateProductdResult>;

public record CreateProductdResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductdResult>
{
    public async Task<CreateProductdResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductdResult(product.Id);
    }
}
