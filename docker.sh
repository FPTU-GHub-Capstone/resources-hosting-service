docker network create -d bridge ghub-bridge-network

docker run --name mssql -u root -h mssql -e SA_PASSWORD=e@x2Prn9mj4zU3ap8WV7ckNK -e ACCEPT_EULA=Y --network ghub-bridge-network mcr.microsoft.com/mssql/server:2019-latest

docker build -t ghub-api .

docker run --name ghub-api --network ghub-bridge-network -e AppSettings__ConnectionStrings__DefaultConnection="Server=mssql,1433;Database=GHub;User Id=sa;Password=e@x2Prn9mj4zU3ap8WV7ckNK;" -p 8080:80 ghub-api