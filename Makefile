.PHONY: test-db-up test-db-down migrate test

test-db-up:
	docker rm -f demoapi-test-db || true
	docker run -d \
	  --name demoapi-test-db \
	  -p 5433:5432 \
	  -e POSTGRES_DB=demoapi_test \
	  -e POSTGRES_USER=demo \
	  -e POSTGRES_PASSWORD=demo \
	  postgres:18.1

test-db-down:
	docker rm -f demoapi-test-db || true

migrate:
	ASPNETCORE_ENVIRONMENT=Test \
	ConnectionStrings__Default="Host=localhost;Port=5433;Database=demoapi_test;Username=demo;Password=demo" \
	dotnet run --project DemoApi/DemoApi.csproj migrate

test:
	ASPNETCORE_ENVIRONMENT=Test \
	ConnectionStrings__Default="Host=localhost;Port=5433;Database=demoapi_test;Username=demo;Password=demo" \
	dotnet test
