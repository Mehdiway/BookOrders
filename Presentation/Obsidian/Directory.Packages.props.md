1. Remove packages from csproj file
2. Create file Directory.Package.props with this content

`<Project>`
  `<PropertyGroup>`
    `<ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>`
  `</PropertyGroup>`
`</Project>`

3. Add back the packages :
	1. Microsoft.AspNetCore.OpenApi
	2. Swashbuckle.AspNetCore