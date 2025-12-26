# ToDoApi

API RESTful para gerenciamento de tarefas ToDo.

Este projeto é uma API construída em **ASP.NET Core** que expõe endpoints para criação, leitura, atualização e deleção de tarefas (ToDos). A API utiliza **Entity Framework Core** com **SQL Server** como banco de dados.

---

## 💡 Funcionalidades

- Criar tarefas
- Leitura de tarefas
- Atualizar tarefas
- Excluir tarefas

---

## 🚀 Pré-requisitos

Antes de começar, certifique-se de ter:

✔️ [.NET 9 SDK](https://dotnet.microsoft.com/download)  
✔️ SQL Server local (ex.: *SQL Server Express*)  
✔️ IDE como Visual Studio ou VS Code  
✔️ Ferramentas CLI do .NET, caso não use IDE  

---

## 📦 Instalação

1. Clone o repositório:
    ```bash
    git clone https://github.com/JoaoBontempo/ToDoApi.git
    cd ToDoApi
    ```

2. Crie um arquivo `.env` na raiz com suas credenciais SQL Server:

    ```env
    DB_SERVER=
    DB_NAME=
    DB_USER=
    DB_PASSWORD=
    ```

## 🛠️ Preparar e Rodar

### 📌 Restaurar pacotes
```bash
dotnet restore
