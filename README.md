
# Descrição do projeto

Projeto em dotnet core 3.1 utilizando Hosted Services para criar um app que produz e consome mensagens através de um servidor Kafka.

O app funciona como um serviço que fica executando indefinidamente, aguardando e enviando mensagens em um tópico específico no servidor Kafka. As mensagens são enviadas a cada 5 segundos contendo um ID do app, o texto Hello World, um ID da mensagem e o data e horário em que a mensagem foi criada. Caso outros apps sejam iniciados, todos possuirão o mesmo comportamento.

Utilizei docker para subir imagens do Kafka, Zoekeeper e do próprio app criado para realizar testes, o script docker compose possui todos os itens necessários para iniciar os containers. As mensagens dos apps serão apresentadas no console do Docker (desktop). 

# Diretórios

 **Kafka.Tests.HostedService** - Projeto com o o HostedService para iniciar o app. 
 **Kafka.Tests.Core** - Projeto com os serviços e interfaces que são injetados no app. 
 **Kafka.Tests.Data** - Projeto com os modelos de dados que são trafegados no app. 
 **Kafka.Tests.Tests** - Projeto básico com testes em alguns serviços do projeto Core.

# Referências utilizadas

 **dotnet add package Microsoft.Extensions.DependencyInjection** 
 **dotnet add package Microsoft.Extensions.Configuration** 
 **dotnet add package Microsoft.Extensions.Hosting** 
 **dotnet add package Serilog.Sinks.Console** 
 **dotnet add package Confluent.Kafka -version 1.4.4** 
 **dotnet add package Moq** 
 **dotnet add package FluentAssertions**

# Comandos para o Docker

Abaixo se encontram os comandos para criar o container do app no Docker e iniciar o processo com 10 instâncias do app para testes.

>Criar imagem do projeto dotnet.
**docker build -t kafka-tests-hostedservice .**

>Atribuir tag a imagem criada
**docker tag kafka-tests-hostedservice:latest kafka-tests-hostedservice:dev**

>Executa o compose com a última versão da image do dotnet criada e dispara 10 instâncias do hostedservice dotnet para fins de teste
**docker-compose up -d --scale kafka-tests-hostedservice=10**
