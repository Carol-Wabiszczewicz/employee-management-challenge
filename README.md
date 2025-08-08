# 📘 Employee Management

Aplicação full stack para gerenciamento de funcionários, desenvolvida com:

- **Frontend:** React + TypeScript + Zod + React Hook Form + Storybook  
- **Backend:** ASP.NET Core (.NET 8)  
- **Banco de Dados:** PostgreSQL  
- **Gerenciamento de containers:** Docker / Docker Compose

---

## 🛠️ Pré-requisitos

Antes de rodar o projeto, certifique-se de ter instalado:

- [Node.js](https://nodejs.org/) `v18.x` ou superior
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [Git](https://git-scm.com/)

---

## 🚀 Como rodar o projeto localmente

### 🔹 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/employee-management.git
cd employee-management
```

---

### 🔹 2. Rode com Docker (recomendado)

Execute o seguinte comando na raiz do projeto:

```bash
docker-compose up --build
```

> 🐳 Isso irá:
> - Subir o frontend (`React`)
> - Subir o backend (`.NET 8`)
> - Subir o banco de dados (`PostgreSQL`)
> - Mapear as portas:  
>   - Frontend: `http://localhost:3000`  
>   - Backend API: `http://localhost:5000`  
>   - Banco: `localhost:5432` (usuário: `postgres`, senha: `postgres`)

---

### 🔹 3. Instalar dependências (modo manual, sem Docker)

Se quiser rodar localmente sem Docker:

#### Frontend

```bash
cd frontend/employee-management
npm install
npm run dev
```

#### Backend

```bash
cd backend/EmployeeManagement.Api
dotnet restore
dotnet run
```

---

## 🧪 Testes e Storybook

### Rodar o Storybook

```bash
cd frontend/employee-management
npm run storybook
```

### Rodar os testes unitários

```bash
npm run test
```

---

## ⚙️ Estrutura do projeto

```
employee-management/
│
├── backend/
│   └── EmployeeManagement.Api/        # Projeto .NET 8 com controllers, services e migrations
│
├── frontend/
│   └── employee-management/
│       ├── src/
│       │   ├── components/            # Componentes React + Storybook
│       │   ├── pages/                 # Páginas da aplicação
│       │   └── schemas/               # Schemas Zod
│       ├── public/
│       └── package.json
│
├── docker-compose.yml
└── README.md
```

---

## 📝 Observações

- Caso use WSL ou Linux, garanta que as permissões das pastas de volume estejam corretas para evitar problemas de cache/pastas node_modules.
- As portas utilizadas nos containers podem ser configuradas no `docker-compose.yml`.

---

## ✅ Checklist pós-clone

- [x] `git clone` do repositório  
- [x] Rodar `docker-compose up` ou `npm install`  
- [x] Acessar `http://localhost:3000`