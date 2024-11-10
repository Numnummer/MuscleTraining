# Itis.MyTrainings.Api

## Интегрированные технологии

- [ASP.NET Core](https://docs.microsoft.com/ru-ru/aspnet/core/?view=aspnetcore-5.0)
- .NET 7
- [EF Core](https://docs.microsoft.com/ru-ru/ef/core/) + Migrations - ORM
- [Mediatr](https://github.com/jbogard/MediatR) - SQRS mediator library
- [xUnit](https://xunit.net/) unit & functional testing

## Структура проекта

Решение разбито на две группы проектов: src (исполняемый код) и test (модульные и интеграционные тесты). Решение побито на проекты в соответствии с [onion architecture pattern](https://www.codeguru.com/csharp/csharp/cs_misc/designtechniques/understanding-onion-architecture.html).

### Itis.MyTrainings.Api.Contracts

Проект с бизнес-контрактами приложения. Не зависит от других проектов. Представляет собой набор контрактов приложения (классов, определяющих запросы и ответы, обрабатываемые приложением и составляющие его внешний интерфейс API).

### Itis.MyTrainings.Api.Core

Проект с бизнес-логикой приложения. Не зависит от других проектов, кроме контрактов. Для использования баз данных и других внешних сервисов задает абстракции в каталоге /Abstractions. Доменная модель приложения - сущности и объекты-значения содержатся в /Entities. Обработчики запросов Mediatr в /Requests, разбиты по каталогам на каждую сущность для удобства навигации. Сервисы приложения в каталоге /Services.

### Itis.MyTrainings.Api.Data.PostgreSql

Проект с реализацией доступа к данным в терминах БД PostgreSql. Использует EF Core для обращения к БД. Для генерации миграций есть скрипты в /Tools.

### Itis.MyTrainings.Api.Web

Проект-веб хост приложения. Конфигурация разных аспектов работы web-приложения реализована в отдельных каталогах. Переменные приложения заведены в appsettigs.json, переопределяются в UserSecrets для отладки.

### Itis.MyTrainings.Api.UnitTests

Проект с модкльными тестами. Для тестов используется xUnit.

