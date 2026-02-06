# DemoApi

ASP.NET Core backend with:
- EF Core + PostgreSQL
- Docker & docker-compose
- Health checks (liveness & readiness)
- JWT authentication

## Run locally
```bash
docker compose up --build
```


Swagger : http://localhost:8080/swagger/index.html


make test-db-up migrate test test-db-down


Runner:
'self-hosted', 'macOS', 'X64' 

cd ~/github-runners/dotnet-runner
./run.sh

