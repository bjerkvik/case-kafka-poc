# Forbedringer

## Autentisering og autorisering
APIet er åpent for alle. I produksjon bør endepunktet beskyttes slik at kun autoriserte klienter kan sende data.

## Konfigurasjon og secrets
`BootstrapServers` er hardkodet i koden. I produksjon bør dette og andre secrets/variabler hentes fra en secret manager som Azure Key Vault, Github secrets eller lignende.

## Logging
Feil logges kun til konsollen. I produksjon trenger man skikkelig logging, metrics og varsling slik at man oppdager problemer.

## Rate limiting
APIet har ingen begrensning på trafikk. I produksjon bør man begrense antall requests per klient for å beskytte serverne.

## Tester
Det er ingen tester. Man bør ha enhetstester for valideringslogikken og integrasjonstester som verifiserer at meldinger faktisk når Kafka.

## Consumer
Dataen publiseres til Kafka men prosesseres ikke videre. En consumer-tjeneste må bygges for å lese meldingene og prosessere de videre.

## Deployment
Tjenesten kjører kun lokalt. Den må deployes til et skymiljø som Azure med CI/CD-pipeline og skikkelig branch-strategi (dev, main).

## IaC
Infrastrukturen bør defineres som kode med f.eks. Terraform, slik at miljøer kan reproduseres og versjonskontrolleres.
