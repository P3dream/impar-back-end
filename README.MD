# Projeto Back-end usando .NET 6

Este projeto é uma API desenvolvida em .NET 6 que oferece as seguintes funcionalidades:

- Operações CRUD para entidades
- Endpoint para recuperar todos os registros com paginação
- Utilização do Entity Framework para mapeamento das entidades no banco de dados
- Desenvolvimento em camadas (Controller, Model, Service, DbContext)
- Utilização de DTOs para manipulação de requisições
- Tratamento de exceções
- Documentação dos endpoints utilizando Swagger
- Autenticação usando JWT (JSON Web Tokens)
- Endpoints de login e cadastro, armazenando a senha criptografada

## Instalação e Execução

Para executar o projeto localmente, siga estas etapas:

1. Clone o repositório em seu diretório local:
   ```bash
   git clone https://github.com/seu-usuario/seu-projeto.git
   ```
2. Abra o projeto em sua IDE preferida.

3. Certifique-se de ter o SDK do .NET 6 instalado. Se não tiver, você pode baixá-lo [aqui](https://dotnet.microsoft.com/download).

4. Configure sua conexão com o banco de dados no arquivo `appsettings.json`.

5. Execute o comando para rodar o projeto:
   ```bash
   dotnet run
   ```
6. Após executar o comando acima, a API estará disponível no endereço `http://localhost:{porta}`.

## Documentação dos Endpoints

Para visualizar a documentação dos endpoints, acesse `http://localhost:{porta}/swagger`.

## Contato

Se tiver dúvidas, sugestões ou comentários, entre em contato através do email pedropizzi23@Hotmail.com