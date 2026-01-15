# 🧪 FoodCall.Tests

Projeto de testes unitários para a aplicação FoodCall.

## 📊 Estatísticas
- **Total de Testes**: 28
- **Framework**: xUnit
- **Cobertura**: Domain, Application, API

## 🛠️ Tecnologias Utilizadas

### Frameworks de Teste
- **xUnit** - Framework de testes unitários
- **FluentAssertions** - Biblioteca para assertions fluentes e legíveis
- **Moq** - Framework para criação de mocks e stubs
- **Microsoft.AspNetCore.Mvc.Testing** - Para testes de integração de API

### Pacotes Adicionais
- **Microsoft.EntityFrameworkCore.InMemory** - Para testes com DbContext em memória

## 📁 Estrutura dos Testes

```
FoodCall.Tests/
├── Domain/               # Testes das entidades de domínio
│   ├── UserTests.cs     # 8 testes - validações de User
│   └── OrderTests.cs    # 8 testes - validações de Order
├── Application/          # Testes dos handlers (CQRS)
│   ├── CreateUserCommandHandlerTests.cs  # 3 testes
│   └── GetUserByIdQueryHandlerTests.cs   # 2 testes
└── API/                  # Testes dos controllers
    └── UsersControllerTests.cs           # 3 testes
```

## ✅ Cobertura de Testes

### Domain (Entidades)
**UserTests.cs** - 8 testes
- ✅ Criação de usuário com dados válidos
- ✅ Validação de nome (vazio, nulo, espaços)
- ✅ Validação de email (vazio, nulo, formato inválido)
- ✅ Validação de telefone (vazio, nulo, espaços)
- ✅ Adição de endereço válido
- ✅ Rejeição de endereço nulo
- ✅ Atualização de nome
- ✅ Atualização de telefone

**OrderTests.cs** - 8 testes
- ✅ Criação de pedido com dados válidos
- ✅ Adição de item ao pedido
- ✅ Validação de quantidade inválida
- ✅ Validação de preço negativo
- ✅ Cálculo correto do total com múltiplos itens
- ✅ Total zero quando sem itens
- ✅ Confirmação de pedido
- ✅ Mudança de status para preparação

### Application (Use Cases)
**CreateUserCommandHandlerTests.cs** - 3 testes
- ✅ Criação de usuário com dados válidos
- ✅ Exceção quando email já existe
- ✅ Hash de senha antes de salvar (BCrypt)

**GetUserByIdQueryHandlerTests.cs** - 2 testes
- ✅ Retorno de usuário existente
- ✅ Exceção quando usuário não encontrado

### API (Controllers)
**UsersControllerTests.cs** - 3 testes
- ✅ Criação de usuário retorna 201 Created
- ✅ Busca de usuário retorna 200 OK
- ✅ Busca de usuário inexistente lança exceção

## 🚀 Como Executar os Testes

### Executar todos os testes
```bash
cd NET_8
dotnet test
```

### Executar com detalhes
```bash
dotnet test --verbosity detailed
```

### Executar testes específicos
```bash
# Por projeto
dotnet test --filter FullyQualifiedName~FoodCall.Tests.Domain

# Por classe
dotnet test --filter FullyQualifiedName~UserTests

# Por método
dotnet test --filter FullyQualifiedName~User_ShouldBeCreated_WithValidData
```

### Ver cobertura de código
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## 🎯 Padrões de Teste Utilizados

### AAA Pattern (Arrange-Act-Assert)
Todos os testes seguem o padrão AAA para clareza:
```csharp
[Fact]
public void Test_ShouldDoSomething_WhenCondition()
{
    // Arrange - Configuração
    var data = new Data();
    
    // Act - Ação
    var result = data.DoSomething();
    
    // Assert - Verificação
    result.Should().BeTrue();
}
```

### Nomenclatura de Testes
Formato: `MethodName_ShouldExpectedBehavior_WhenStateUnderTest`

Exemplos:
- `User_ShouldBeCreated_WithValidData()`
- `AddItem_ShouldThrowException_WhenQuantityIsInvalid()`

### Mocking com Moq
```csharp
// Setup do mock
_userRepositoryMock
    .Setup(x => x.GetByIdAsync(userId))
    .ReturnsAsync(user);

// Verificação
_userRepositoryMock.Verify(
    x => x.GetByIdAsync(userId), 
    Times.Once
);
```

### Assertions Fluentes com FluentAssertions
```csharp
// Mais legível que Assert.Equal
result.Should().NotBeNull();
result.Name.Should().Be("João Silva");
user.Addresses.Should().HaveCount(1);
```

## 📝 Próximos Passos

### Testes a Adicionar
- [ ] Testes de integração (end-to-end)
- [ ] Testes para RestaurantsController
- [ ] Testes para OrdersController
- [ ] Testes para ProductsController
- [ ] Testes para CouriersController
- [ ] Testes de repositórios
- [ ] Testes de validação de DTOs

### Melhorias
- [ ] Configurar relatório de cobertura de código
- [ ] Adicionar testes de performance
- [ ] Implementar testes de carga
- [ ] Configurar CI/CD para executar testes automaticamente

## 🔧 Configuração do Projeto

### Dependências no .csproj
```xml
<ItemGroup>
  <PackageReference Include="FluentAssertions" Version="8.8.0" />
  <PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="10.0.2" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="10.0.2" />
  <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.13.0" />
  <PackageReference Include="Moq" Version="4.20.72" />
  <PackageReference Include="xunit" Version="2.9.3" />
  <PackageReference Include="xunit.runner.visualstudio" Version="3.1.4" />
</ItemGroup>
```

## 💡 Dicas

1. **Executar testes frequentemente** durante o desenvolvimento
2. **Um teste por comportamento** - testes focados e pequenos
3. **Nomes descritivos** - o teste deve documentar o comportamento
4. **Evitar lógica nos testes** - testes devem ser simples
5. **Independência** - cada teste deve rodar isoladamente
6. **Velocidade** - testes unitários devem ser rápidos

## 📚 Referências

- [xUnit Documentation](https://xunit.net/)
- [FluentAssertions](https://fluentassertions.com/)
- [Moq Quickstart](https://github.com/moq/moq4)
- [Unit Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
