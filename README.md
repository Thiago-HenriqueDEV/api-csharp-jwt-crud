api-csharp-jwt-crud
API REST desenvolvida em C# com operações de CRUD e sistema de autenticação segura utilizando tokens JWT.

Este projeto marca o meu primeiro desenvolvimento prático de uma API. O objetivo principal foi colocar a mão na massa com C# e .NET, focado em entender a fundo como estruturar rotas, realizar operações de banco de dados (CRUD) e implementar uma camada real de segurança e controle de acesso utilizando tokens JWT.

Começamos entendendo de fato o que seria uma API, e seguimos no exemplo de:

Backend     =>     API    =>   FrontEnd
Cozinheiro  =>    Garçom   =>   Cliente

Logo após, aprendemos os nossos métodos e como eles funcionam fazendo alterações e ilustrando o nosso banco de dados (usamos muito a expressão lambda para identificação e alteração de produtos no nosso CRUD):

- POST: Adição de novos registros no sistema.

-  GET: Listagem completa e busca detalhada através do ID do produto nos registros.

- PUT: Atualização de informações de registros existentes a partir do ID.

- DELETE: Exclusão segura de registros a partir do ID.

Decidimos criar uma API de início com atualização em nuvem, ou seja, após desligá-la, todo progresso era perdido.

 - Criamos uma pequena lista privada de produtos.

 - Adicionamos o Swagger, para termos a visualização das nossas atualizações fora do formato JSON.

 - Adicionamos os nossos métodos e visualizamos rodando nossa API pela primeira vez.

#Estruturação

Começamos a estruturar uma API REST, para trazer clareza e facilidade de manutenção no código, trazendo pastas separadas e adicionando Controllers, Services e Database.

- Nos nossos Controllers, adicionamos os nossos métodos HTTP que antes ficavam diretamente no Program.

- Tive que atualizar a minha lista para static.

- Agora adicionei os Services para separar o modo de leitura HTTP e deixar nossa lista de forma separada.

- Criei uma interface para trazer obrigatoriedade dos nossos métodos e nomenclatura apropriada para cada através da herança.

- Rodamos a API novamente para ter certeza de que tudo está funcionando perfeitamente.

Saindo da ideia de ter apenas um armazenamento de dados temporário, começamos a aprender sobre CRUD usando banco de dados, no caso SQLite.

- Criamos um builder no nosso Program, para trazer integridade com o banco de dados.

- Criamos nossas migrações com comandos de terminal.

- Criamos a classe ProdutosDataBaseService.cs e configuramos trazendo novos métodos.

- E adicionamos o retorno do Program para um builder de ProdutosDataBaseService.
(Acabei dando de cara com um erro 500, porque criei a migração sem salvar as atualizações do Program, então tive que salvar e subir uma nova migração).

- API rodando junto do Banco de Dados.

Agora indo para a parte corporativa, aprendemos sobre validações e regras de negócio.

- Implementei uma validação simples na base dos meus métodos, que requeria um valor mínimo e máximo, e para os métodos de string uma limitação de caractere, junto de um Required para obrigatoriedade de preenchimento de campo.

- Para regra de negócio, trouxemos uma limitação de inserção de produto, no caso coloquei que "Celular" não poderia ser adicionado e sempre vai bater no erro 500.

- Partindo para a parte de segurança, resolvemos adicionar login e tokens com limitação de 1 hora.

- Configuramos o JWT junto do Program.

- Criamos o endpoint de login com uma classe chamada LoginController.
(Dentro dessa pasta criamos dois tipos de usuários, sendo eles "Clientes" e "Admin").

- Configuramos o Swagger para aceitar os tokens.

- Configuramos o ProdutosController, para entender que alguns métodos precisam de autorização.

Apenas para mantermos o padrão de API REST e boas práticas:

- Criamos uma classe Models e adicionamos uma interface de login.

- Junto da interface de Produtos.



## Ferramentas e Tecnologias Utilizadas

Durante o desenvolvimento deste projeto, eu utilizei as seguintes tecnologias para construir e testar a API:

- C# e .NET: A linguagem de programação e o framework base que escolhi para o desenvolvimento de toda a estrutura do projeto.

- JWT (JSON Web Token): Tecnologia que implementei para criar a camada de segurança, gerenciar o login e gerar os tokens de autorização para os nossos usuários (Clientes e Admin).

- Swagger: Ferramenta que adicionei para gerar uma interface visual interativa da API. Ela me permitiu testar e visualizar os métodos (endpoints) diretamente no navegador, fora do formato JSON puro.

- SQLite: O sistema de banco de dados que utilizei para sair do armazenamento temporário em memória e passar a guardar os registros do CRUD de forma permanente.

- Migrations: Utilizei os comandos de terminal para gerar as migrações e espelhar as configurações que fiz no código diretamente na estrutura do banco de dados real.

- Expressões Lambda: Recurso do C# que usei intensamente no código para facilitar a busca, identificação e manipulação dos dados dos produtos.
