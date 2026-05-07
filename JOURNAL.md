# Journal

## Første tanker

Oppgaven virket relativt rett fram. Et HTTP-endepunkt som tar imot JSON og sender det videre til Kafka. Hovedutfordringen for meg var at jeg ikke hadde jobbet med Kafka før, så jeg brukte litt tid på å forstå hvordan en producer fungerer og hvordan meldinger faktisk sendes.

## Språkvalg

Jeg valgte C# fordi det er det jeg har jobbet mest med i det siste. Jeg vurderte Python siden jeg kanskje er litt mer komfortabel der, men C# med .NET føltes som et mer naturlig valg for en API.

## Underveis

Selve API-strukturen var grei å sette opp med "dotnet new webapi" som utgangspunkt. Det meste av spørsmålene kom fra Kafka-siden: hvordan produceren konfigureres, hva de ulike parametrene som `Acks` og `BootstrapServers` betydde, og hvordan meldinger bekreftes.

Produceren registreres som en singleton siden man ikke ønsker å opprette en ny producer per request. Kafka-konfigurasjonen holder seg til `BootstrapServers` mot `localhost:9094` som eksponeres av docker-compose, og ellers bruker defaults.

Jeg la til enkel validering av request-body: tom body gir 400, ugyldig JSON gir 400, og feil mot Kafka gir 503. Jeg la også til et `/health`-endepunkt, siden det er et naturlig behov selv for en enkel tjeneste.

## Resultat

Endte opp med en løsning der JSON-data kan postes til `/events` og bekreftes mottatt av Kafka.
