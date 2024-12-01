Prerequesites
`docker`, `docker-compose`, `git`

1) git clone https://github.com/EfimenkoAndrew/TaxCalculatorTestTask.git
2) cd TaxCalculatorTestTask
3) docker-compose --env-file .env -f docker-compose.yml -f docker-compose.override.yml build
4) docker-compose --env-file .env -f docker-compose.yml -f docker-compose.override.yml up -d
5) visit http://localhost:4201/

enjoy.
the API has the ability to provide calculations history and has integration with RabbitMQ (1http://localhost:5672) guest/guest 
the DB is Postgres.
Transactional Inbox/Outbox are implemented. 
Libraries: 
MassTransit (RabbitMQ)
EF Core (PostgreSQL)
