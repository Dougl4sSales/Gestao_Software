# Gestão de Software

Sistema web para cadastrar softwares e gerenciar suas versões, incluindo data de lançamento e indicação de versões depreciadas.

## 1. Funcionalidades

- Cadastrar softwares com nome e descrição.
- Consultar softwares cadastrados e suas versões.
- Cadastrar novas versões vinculadas a um software.
- Informar número da versão, data de lançamento e se ela está depreciada.
- Editar versões existentes.
- Excluir versões individualmente.
- Excluir softwares, conforme as regras de integridade configuradas na aplicação e no banco de dados.
- Pesquisar softwares e números de versão na interface web.

## 2. Tecnologias

- **C# e .NET:** desenvolvimento do backend e da API.
- **ASP.NET Core Web API:** disponibilização dos endpoints REST.
- **Blazor:** interface web interativa.
- **Entity Framework Core:** acesso a dados.
- **SQL Server:** armazenamento dos softwares e versões.
- **Docker e Docker Compose:** execução dos serviços em contêineres.
- **Swagger / OpenAPI:** documentação e teste dos endpoints da API.

## 3. Estrutura do projeto

A estrutura principal é organizada em duas pastas:

```text
Gestao_Software/
├── Backend/
│   └── src/
│       └── MeuProjeto.API/
├── Frontend/
│   └── GestaoDeSofware.Frontend/
└── docker-compose.yml
```

O projeto frontend utiliza o nome `GestaoDeSofware.Frontend` (grafado “Sofware”, sem o segundo “t”). Os nomes internos podem mudar conforme as alterações feitas no código.

## 4. Pré-requisitos

- Docker Engine ou Docker Desktop.
- Docker Compose, disponível pelo comando `docker compose`.
- Git, caso vá clonar o repositório.

Não é necessário instalar o SQL Server separadamente se ele for iniciado pelo Docker Compose.

## 5. Iniciar o sistema com Docker

### 5.1. Abrir a pasta do projeto

No terminal, entre na pasta raiz do repositório, onde está o arquivo `docker-compose.yml`:

```bash
cd /caminho/para/Gestao_Software
```

### 5.2. Conferir a configuração

Confira se o arquivo `docker-compose.yml` existe e se as variáveis de ambiente exigidas pelo SQL Server estão configuradas. Se o Compose utilizar um arquivo `.env`, preencha os valores necessários antes de iniciar os serviços.

**Importante:** use uma senha forte para o usuário administrador do SQL Server. Não publique senhas ou outros segredos no Git.

### 5.3. Construir e iniciar os serviços

Para construir as imagens e iniciar backend, frontend e banco de dados:

```bash
docker compose up -d --build
```

Para verificar o estado dos contêineres:

```bash
docker compose ps
```

Para acompanhar os logs de todos os serviços:

```bash
docker compose logs -f
```

Para acompanhar somente os logs do backend:

```bash
docker compose logs -f backend
```

Para acompanhar somente os logs do frontend:

```bash
docker compose logs -f frontend
```

A primeira inicialização pode demorar enquanto as imagens são construídas e o banco de dados é inicializado.

## 6. Acessar o sistema

No ambiente de desenvolvimento, as portas configuradas são:

- **Interface web:** http://localhost:8081
- **Swagger da API:** http://localhost:8080/swagger
- **SQL Server:** `localhost,1433` para ferramentas de banco de dados executadas no computador host.(Foi utilizado o Dbeaver)

Esses endereços dependem das portas publicadas no `docker-compose.yml`. Se você alterar o mapeamento de portas, utilize os endereços correspondentes.

O frontend executado em um contêiner normalmente acessa a API pelo nome do serviço Docker, por exemplo `http://backend:8080/`. Esse endereço é destinado à comunicação entre contêineres; no navegador do computador host, utilize a porta publicada, como `http://localhost:8080`.

## 7. Utilizar o Swagger

O Swagger permite consultar os endpoints, visualizar os modelos de dados e enviar requisições para testar a API.

1. Inicie o sistema com Docker Compose.
2. Abra http://localhost:8080/swagger no navegador.
3. Expanda o grupo de endpoints desejado.
4. Clique em **Try it out**.
5. Preencha os parâmetros ou o corpo JSON solicitado.
6. Clique em **Execute**.
7. Confira o código HTTP e o corpo da resposta.

Se o Swagger não abrir, confira se o backend está em execução, se a porta `8080` está publicada e se a configuração do Swagger está habilitada no ambiente atual. Consulte também os logs do backend.

## 8. Principais endpoints da API

As rotas abaixo correspondem aos endpoints utilizados no projeto. Confirme no Swagger quais operações estão disponíveis e quais campos são obrigatórios na versão atual do código.

### Softwares

| Método | Rota | Finalidade |
|---|---|---|
| `GET` | `/api/softwares` | Listar softwares cadastrados |
| `POST` | `/api/softwares` | Cadastrar um software |
| `PUT` | `/api/softwares/{id}` | Atualizar um software, se implementado nessa rota |
| `DELETE` | `/api/softwares/{id}` | Excluir um software |

### Versões

| Método | Rota | Finalidade |
|---|---|---|
| `GET` | `/api/softwares/{softwareId}/versoes` | Listar as versões de um software |
| `POST` | `/api/softwares/{softwareId}/versoes` | Cadastrar uma versão vinculada a um software |
| `GET` | `/api/versoes/{id}` | Consultar uma versão pelo ID |
| `PUT` | `/api/versoes/{id}` | Atualizar uma versão |
| `DELETE` | `/api/versoes/{id}` | Excluir uma versão |

> O Swagger é a referência para confirmar os contratos atuais, os parâmetros, os campos esperados e os códigos de resposta.

## 9. Exemplos de JSON

Os exemplos são ilustrativos. Ajuste os campos conforme o modelo apresentado no Swagger.

### Cadastrar um software

`POST /api/softwares`

```json
{
  "nome": "Sistema Financeiro",
  "descricao": "Sistema para controle financeiro"
}
```

### Cadastrar uma versão

`POST /api/softwares/1/versoes`

```json
{
  "numeroVersao": "1.0.0",
  "dataRelease": "2026-10-18",
  "depreciado": false
}
```

O número `1` na rota representa o ID do software ao qual a versão será vinculada. Utilize um ID existente no banco de dados. Dependendo das validações e dos campos definidos nos modelos, a API pode exigir informações adicionais.

## 10. Fluxo básico de utilização

1. Abra a interface web.
2. Cadastre um software informando o nome e, opcionalmente, a descrição.
3. No cartão do software, escolha a opção para cadastrar uma nova versão.
4. Informe o número da versão e a data de lançamento.
5. Marque a versão como depreciada quando aplicável.
6. Utilize o lápis para editar uma versão existente.
7. Utilize a lixeira ao lado da versão para solicitar sua exclusão e confirme a operação.
8. Use a busca para localizar softwares ou números de versão.

Excluir uma versão é diferente de excluir o software. Confira o item selecionado antes de confirmar uma exclusão.

## 11. Parar e reiniciar o sistema

Para parar e remover os contêineres em execução:

```bash
docker compose down
```

Para iniciar novamente os serviços já configurados:

```bash
docker compose up -d
```

Para reconstruir e iniciar somente o frontend após alterações nele:

```bash
docker compose up -d --build frontend
```

Para reconstruir e iniciar somente o backend após alterações nele:

```bash
docker compose up -d --build backend
```

**Atenção:** evite `docker compose down -v` se quiser preservar os dados armazenados em volumes do banco. A opção `-v` remove os volumes associados e pode apagar os dados persistidos.

## 12. Diagnóstico de problemas

### Consultar os logs

```bash
docker compose logs --tail=200 backend
docker compose logs --tail=200 frontend
docker compose logs --tail=200 sqlserver
```

### Conferir os contêineres

```bash
docker compose ps
```

### Swagger ou API não respondem

- Confira se o contêiner do backend está em execução.
- Verifique se a porta `8080` está publicada no Compose.
- Consulte os logs do backend.
- Confirme que o Swagger está habilitado na configuração do ambiente.

### Erros de conexão com o banco

- Confira se o serviço `sqlserver` está em execução e terminou sua inicialização.
- Dentro da rede Docker Compose, a connection string deve usar o nome do serviço, por exemplo `sqlserver`, em vez de `localhost`.
- Verifique o nome do banco, usuário e senha.
- Confira se as migrações do Entity Framework Core foram aplicadas, caso o projeto dependa delas.

### Erro ao excluir um software ou versão

Leia a mensagem retornada pela API e consulte os logs do backend. Uma restrição de chave estrangeira pode impedir a exclusão de registros relacionados, dependendo das regras configuradas no banco e na aplicação.

## 13. Boas práticas

- Não versionar senhas, tokens ou outras credenciais.
- Validar os dados enviados à API.
- Consultar o Swagger para verificar os contratos e as respostas dos endpoints.
- Manter os dados do SQL Server em um volume persistente quando eles precisarem sobreviver à recriação dos contêineres.
- Consultar os logs sempre que ocorrerem erros de inicialização ou de requisição.

---

## 14. Gerar Migration no Docker
- dotnet ef migrations add InitialCreate
Projeto de estudo para gerenciamento de softwares e suas versões.
