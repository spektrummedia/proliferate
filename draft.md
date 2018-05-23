# proliferate


## But

Une application `serverless` qui s'occupe de l'envoi des courriels de l'ensemble de nos projets d'agence. On remplace donc l'utilisation du SMTP tradionel (`SmtpClient.Send()`) ou encore de Sendgrid. 

L'aspect pratique face à ce qu'on l'a déjà:

* Visualer des courriels envoyés (comme dans Mandrill)
* Changer de mail provider pour nos ~150 projets à un seul endroit.
* Réduire le temps d'intégration d'un système de mailing dans un projet

## Stack

* C# .NET Core
* AWS Lambda pour exécuter le code
* AWS SES comme SMTP
* AWS DynamoDB comme base de données

On va faire ça en mode OSS.


## Étapes

On peut diviser le projet en 3 étapes. Ce qui est cool, c'est qu'on peut avoir quelque chose en prod en *très peu de temps*.


### Étape 1 (Court terme, easy shot. Prêt rapidement.)

Backend:
* Fonction basique qui prend en paramètre les infos nécessaires (to, from, body) et s’occupe de l’envoi par SES. Un event est lancé à travers SNS afin de stocker l'information dans la base de données.
* Permettre la génération d'une API key afin de segmenter les clients


Client:
* Nuget package qui s’occupe d'envoyer la requête HTTP vers notre fonction. La librairie prend en paramètre une API key (et éventuellement un endpoint AWS)


### Étape 2 (moyen / long terme)

* Dashboard simpliste qui s'occupe de passer des requêtes sur le data accumulé pour sortir des stats d’envois par clients
* Permettre de visionner les courriels envoyés (screw you, Sendgrid!)

Possibilité pour le dashboard:
* http://www.cyclotron.io/index.html


### Étape 2 (moyen / long terme)

* Pouvoir renvoyer un courriel
* Suivi des bounces
* Validation des courriels entrants (éviter d'essayer un envoi lorsqu'un domaine est inexistant, etc.)