
1. Create the proto file in Shared/Protos:

```
syntax = "proto3";

option csharp_namespace = "Shared.Grpc";

package catalog;

service CatalogService {
  rpc CheckBookQuantity (CheckBookQuantityRequest) returns (CheckBookQuantityResponse);
}

message CheckBookQuantityRequest {
  int32 book_id = 1;
  int32 quantity = 2;
}

message CheckBookQuantityResponse {
  bool is_available = 1;
}
```

2. Install these dependencies :
	1. Grpc.AspNetCore
	2. Google.Protobuf
	3. Grpc.Net.Client
	4. Grpc.Tools

3. Include in Shared.csproj :
```
<ItemGroup>
	<Protobuf Include="Protos\catalog.proto" GrpcServices="Both" />
</ItemGroup>
```

__Catalog__ :

1. Create CatalogGrpcService :
```
namespace Catalog.Infrastructure.Services;

public class CatalogGrpcService : CatalogService.CatalogServiceBase
{
    private readonly IBookRepository _bookRepository;

    public CatalogGrpcService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public override async Task<CheckBookQuantityResponse> CheckBookQuantity(CheckBookQuantityRequest request, ServerCallContext context)
    {
        var book = await _bookRepository.GetBookByIdAsync(request.BookId);
        Guard.Against.Null(book);

        return new()
        {
            IsAvailable = book.Quantity >= request.Quantity
        };
    }
}

```

2. Setup Grpc in Catalog service's Program.cs :
```
builder.Services.AddGrpc();
...
app.MapGrpcService<CatalogGrpcService>();
```

__Order :__
1. Setup Grpc in Program.cs :
```
var catalogServiceUrl = builder.Configuration.GetConnectionString("CatalogService");
builder.Services.AddSingleton(provider =>
{
    return GrpcChannel.ForAddress(catalogServiceUrl!);
});
```
2. In CheckoutBookCommandHandler :
	1. Inject GrpcChannel channel
	2. Create a client in the constructor :
```
_client = new CatalogService.CatalogServiceClient(channel);

...

private async Task CheckBookQuantityUsingGrpcAsync(int bookId, int quantity)
{
    var request = new CheckBookQuantityRequest { BookId = bookId, Quantity = quantity };
    var response = await _client.CheckBookQuantityAsync(request);

    if (!response.IsAvailable)
    {
        throw new InsufficientQuantityException();
    }
}
```
