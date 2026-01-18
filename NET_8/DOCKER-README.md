# FoodCall API - Docker Deployment

Este guia mostra como fazer deploy da API FoodCall usando Docker e Docker Compose.

## Pré-requisitos

- Docker Desktop instalado
- Docker Compose instalado

## Arquivos de Configuração

O projeto inclui os seguintes arquivos para Docker:

- **Dockerfile**: Define a imagem da aplicação .NET
- **.dockerignore**: Especifica arquivos a serem ignorados durante o build
- **docker-compose.yml**: Orquestra todos os serviços (API, PostgreSQL, pgAdmin)

## Como Usar

### 1. Build e Executar com Docker Compose

No diretório `NET_8`, execute:

```powershell
docker-compose up --build
```

Este comando irá:
- Baixar as imagens necessárias (PostgreSQL, pgAdmin)
- Fazer build da aplicação .NET
- Criar e iniciar todos os containers
- Configurar a rede entre os serviços

### 2. Executar em Background

Para executar os containers em background:

```powershell
docker-compose up -d --build
```

### 3. Verificar Logs

Para ver os logs da API:

```powershell
docker-compose logs -f foodcall-api
```

Para ver logs de todos os serviços:

```powershell
docker-compose logs -f
```

### 4. Parar os Containers

```powershell
docker-compose down
```

Para parar e remover também os volumes (dados do banco):

```powershell
docker-compose down -v
```

## Serviços Disponíveis

Após iniciar os containers, os seguintes serviços estarão disponíveis:

| Serviço | URL | Descrição |
|---------|-----|-----------|
| API | http://localhost:5000 | FoodCall API |
| Swagger | http://localhost:5000/swagger | Documentação da API |
| PostgreSQL | localhost:5432 | Banco de dados |
| pgAdmin | http://localhost:5050 | Interface web para gerenciar o PostgreSQL |

### Credenciais do pgAdmin

- **Email**: admin@foodcall.com
- **Senha**: admin123

### Credenciais do PostgreSQL

- **Host**: foodcall-db (dentro do Docker) ou localhost (fora do Docker)
- **Porta**: 5432
- **Usuário**: postgres
- **Senha**: postgres123
- **Database**: foodcall

## Aplicar Migrations

As migrations serão aplicadas automaticamente quando a aplicação iniciar (se configurado no Program.cs), ou você pode aplicá-las manualmente:

### Dentro do container:

```powershell
# Entrar no container da API
docker exec -it foodcall-api sh

# Executar as migrations
dotnet ef database update --project /src/FoodCall.Infrastructure --startup-project /src/FoodCall.API
```

### Do host (se preferir):

```powershell
# Na pasta NET_8
dotnet ef database update --project FoodCall.Infrastructure --startup-project FoodCall.API
```

O arquivo SQLite será criado em `/app/data/foodcall.db` dentro do container e persistido no volume Docker.

## Build apenas da Imagem Docker

Para fazer build apenas da imagem sem usar docker-compose:

```powershell
docker build -t foodcall-api:latest .
```

Para executar o container manualmente:

```powershell
docker run -d -p 5000:8080 --name foodcall-api foodcall-api:latest
```

## Configurações de Ambiente

As variáveis de ambiente podem ser configuradas no arquivo `docker-compose.yml`:

- **ASPNETCORE_ENVIRONMENT**: Define o ambiente (Development, Production)
- **ConnectionStrings__DefaultConnection**: Caminho do arquivo SQLite (Data Source=/app/data/foodcall.db)
- **JwtSettings__SecretKey**: Chave secreta para geração de tokens JWT
- **JwtSettings__Issuer**: Emissor do token
- **JwtSettings__Audience**: Audiência do token
- **JwtSettings__ExpiresInMinutes**: Tempo de expiração do token em minutos

⚠️ **IMPORTANTE**: Altere a chave secreta JWT em produção!

## Troubleshooting

### Container não inicia

Verifique os logs:
```powershell
docker-compose logs foodcall-api
```

### Erro de permissão no SQLite

Se houver erro de permissão ao criar o banco:
```powershell
docker-compose down -v
docker-compose up --build
```

### Reconstruir a imagem do zero

```powershell
docker-compose build --no-cache
docker-compose up
```

### Limpar tudo e recomeçar

```powershell
docker-compose down -v
docker system prune -a
docker-compose up --build
```

## Produção

Para produção, considere:

1. Usar variáveis de ambiente seguras (secrets)
2. Configurar HTTPS/SSL
3. Usar um banco de dados gerenciado
4. Implementar health checks
5. Configurar logs centralizados
6. Usar um orquestrador como Kubernetes para escalar
