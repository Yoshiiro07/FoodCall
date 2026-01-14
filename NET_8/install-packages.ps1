# Comandos para adicionar pacotes NuGet necessários

# Na pasta FoodCall.API
cd d:\Projetos\FoodCall\NET_8\FoodCall.API
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package BCrypt.Net-Next

# Na pasta FoodCall.Application
cd d:\Projetos\FoodCall\NET_8\FoodCall.Application
dotnet add package BCrypt.Net-Next
dotnet add package System.IdentityModel.Tokens.Jwt

# Restaurar pacotes em todo o projeto
cd d:\Projetos\FoodCall\NET_8
dotnet restore
