build:
	@dotnet build

run:
	@dotnet run

clean:
	@dotnet clean

tunnel:
	@ngrok http 4000

test:
	@dotnet test