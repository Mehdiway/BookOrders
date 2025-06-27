```
builder.Services.AddHttpClient("catalog", client =>
{
    client.BaseAddress = new Uri("https://localhost:7000");
});
```

```
    public OrdersController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("catalog");
    }
...
var books = await _httpClient.GetFromJsonAsync<List<BookDto>>("/api/books")
```