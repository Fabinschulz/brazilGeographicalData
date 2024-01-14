# Sistema de Informações Geográficas

Este projeto consiste em um repositório que contém dados sobre cidades e estados de todo o Brasil.

## Tecnologia e Dependências

- **.NET 7**
- **Docker**
- **Auto Mapper**
- **Fluent Validation**
- **MediatR**
- **Entity Framework Core**
- **Autenticação com JWT Bearer**
- **Tokens JWT**
- **Injeção de Dependência**

## Configuração do Docker Compose

Para facilitar o desenvolvimento e testes, recomendamos a utilização do Docker com um contêiner SQLite. Utilize o seguinte comando para iniciar o contêiner:

```bash
docker-compose up -d
```
Para configurar o Docker Compose, adicione o seguinte código ao seu arquivo `docker-compose.yml`:

```yaml
version: '3'
services:
  sqlite3:
    image: nouchka/sqlite3:latest
    stdin_open: true
    tty: true
    volumes:
      - ./db/:/root/db/
```


## Referências

Para entender melhor as práticas adotadas e as tecnologias utilizadas, recomendo a leitura das seguintes referências:

- [ASP.NET Core CQRS Mediator](https://balta.io/blog/aspnet-core-cqrs-mediator)
- [Tutorial ASP.NET Core - Min Web API](https://learn.microsoft.com/pt-br/aspnet/core/tutorials/min-web-api?view=aspnetcore-8.0&tabs=visual-studio)
- [Rotas com Handlers - ASP.NET Core](https://learn.microsoft.com/pt-br/aspnet/core/fundamentals/minimal-apis/route-handlers?view=aspnetcore-8.0)
