# Kafka PoC – Backend API

Et enkelt HTTP API som tar imot vilkårlig JSON og publiserer det til Kafka.

## Krav

- [.NET 9](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)

## Kom i gang

Start Kafka:

```bash
docker compose up -d
```

Start API-et:

```bash
cd KafkaApi
dotnet run
```

## Endepunkter

| Metode | Sti       | Beskrivelse             |
| ------ | --------- | ----------------------- |
| `POST` | `/events` | Publiser JSON til Kafka |
| `GET`  | `/health` | Helsesjekk              |

### Eksempel

```bash
curl -X POST http://localhost:5146/events \
  -H "Content-Type: application/json" \
  -d '{"userId": "123", "action": "click"}'
```

## Verifisering

Kjør PowerShell-scriptet for å teste endepunktene:

```powershell
.\verify.ps1
```

## Dokumentasjon

- [JOURNAL.md](JOURNAL.md) – Tanker og valg underveis
- [IMPROVEMENTS.md](IMPROVEMENTS.md) – Forbedringer for produksjon
