- [ ] Install package : Swashbuckle.AspNetCore
- [ ] Add in Program.cs :

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
...
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}