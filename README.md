# Simulador da Copa do Mundo 2022

Projeto reorganizado com frontend e backend separados em uma solução mais próxima de um cenário real de avaliação técnica.

## Estrutura

```text
pj_copa/
├── Solution/
│   ├── Backend/
│   │   ├── Controllers/
│   │   ├── DTOs/
│   │   ├── Models/
│   │   ├── Services/
│   │   ├── Properties/
│   │   ├── Backend.csproj
│   │   └── Program.cs
│   └── Frontend/
│       ├── src/
│       │   ├── assets/
│       │   ├── components/
│       │   ├── pages/
│       │   ├── services/
│       │   ├── types/
│       │   └── utils/
│       ├── package.json
│       ├── vite.config.ts
│       └── index.html
├── pj_copa.sln
└── README.md
```

## Backend

O backend em ASP.NET Core atua como orquestrador da lógica e da integração com a API externa.

Principais peças:

- `Controllers/WorldCupController.cs`: endpoints da aplicação
- `Services/KatalystApiService.cs`: consumo da API externa com `IHttpClientFactory`
- `Services/CupService.cs`: sorteio dos grupos
- `Services/SimulationService.cs`: fase de grupos e classificação
- `Services/KnockoutService.cs`: mata-mata e pênaltis
- `Services/WorldCupWorkflowService.cs`: fluxo completo da simulação

### Executar o backend

```bash
cd Solution/Backend
dotnet run
```

Backend local:

- `http://localhost:5000`
- `https://localhost:7001`

## Frontend

O frontend foi estruturado com React + TypeScript + Vite, mantendo a interface modular e mais rica visualmente.

Principais peças:

- `pages/Home.tsx`: orquestra a experiência da tela principal
- `components/GroupCard.tsx`: grupos e classificação
- `components/MatchRow.tsx`: linhas de partidas
- `components/Bracket.tsx`: exibição das fases eliminatórias
- `components/FinalResult.tsx`: resultado final e campeão
- `services/api.ts`: comunicação com o backend

### Executar o frontend

```bash
cd Solution/Frontend
npm install
npm run dev
```

Por padrão, o frontend usa:

```env
VITE_API_BASE_URL=/api
```

O proxy do Vite redireciona `/api` para `http://localhost:5000`.

## Fluxo da aplicação

1. O frontend envia o `git-user` para o backend.
2. O backend busca as seleções na API externa.
3. O backend realiza o sorteio dos grupos.
4. O backend simula fase de grupos, classificação e mata-mata.
5. O backend envia o resultado final para a API externa quando solicitado.
6. O frontend exibe grupos, chaveamento, final e campeão.

