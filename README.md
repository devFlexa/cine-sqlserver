# Sistema de Cinema - .NET/SQLSERVER

Sistema simples para gerenciar **filmes** e **sessões** de cinema, desenvolvido em **C#** utilizando **POO** e **SQL Server**.  

---

## 🏗 Estrutura do Projeto

O projeto segue uma arquitetura organizada por camadas:

DEV_MANHA/
│
├─ Models/ # Classes que representam os dados (Filme, Sessao, Enum Gênero)
├─ Data/ # Abstração da conexão com o banco (Db.cs)
├─ Repositories/ # CRUD direto com o banco (FilmeRepository, SessaoRepository)
├─ Services/ # Lógica do sistema e menu de interação (CinemaService)
├─ Program.cs # Inicialização do serviço
└─ README.md

---

## 📋 Funcionalidades

- Cadastrar filme
- Listar filmes cadastrados
- Atualizar filme
- Excluir filme (junto com suas sessões)
- Cadastrar sessão de um filme
- Listar sessões de um filme específico
- Atualizar sessão
- Excluir sessão individual

---

## 💻 Tecnologias

- **C# 8.0+**
- **.NET 8.0**
- **SQL Server**
- **Console Application**
- **POO e design organizado por camadas**
  
---

## 🗂 Estrutura do Banco de Dados

**Filmes**

| Coluna | Tipo       | Observação               |
|--------|-----------|--------------------------|
| IdFilme | INT      | PK, Identity             |
| Titulo  | NVARCHAR |                          |
| Genero  | NVARCHAR | Enum (Ação, Comédia...) |
| Ano     | INT      |                          |

**Sessoes**

| Coluna  | Tipo      | Observação                      |
|---------|----------|---------------------------------|
| IdSessao | INT     | PK, Identity                     |
| IdFilme  | INT     | FK -> Filmes(IdFilme)           |
| Data     | DATE    | Data da sessão                  |
| Hora     | TIME    | Hora da sessão                  |

---

## ⚙️ Configuração do Projeto

1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/DEV_MANHA.git

2. Abra no Visual Studio ou VS Code.

3. Configure a string de conexão no Program.cs:

using DEV_MANHA.Models.Data;
using DEV_MANHA.Services;

var cs = @"Server=localhost\SQLEXPRESS;Database=CinemaDB;Trusted_Connection=True;";
Db.ConnectionString = cs;

var service = new CinemaService();
service.Executar();

Execute o projeto (dotnet run no terminal).

