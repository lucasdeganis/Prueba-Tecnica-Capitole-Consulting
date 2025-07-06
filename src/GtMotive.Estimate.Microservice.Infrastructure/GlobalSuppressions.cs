// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
// Review

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0290:Usar constructor principal", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Logging.LoggerAdapter`1.#ctor(Microsoft.Extensions.Logging.ILoggerFactory)")]
[assembly: SuppressMessage("Style", "IDE0290:Usar constructor principal", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Telemetry.AppTelemetry.#ctor(Microsoft.ApplicationInsights.TelemetryClient)")]

[assembly: SuppressMessage("Usage", "CA2254:La plantilla debe ser una expresión estática", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Logging.LoggerAdapter`1.LogWarning(System.String,System.Object[])")]
[assembly: SuppressMessage("Usage", "CA2254:La plantilla debe ser una expresión estática", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Logging.LoggerAdapter`1.LogInformation(System.String,System.Object[])")]
[assembly: SuppressMessage("Usage", "CA2254:La plantilla debe ser una expresión estática", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Logging.LoggerAdapter`1.LogError(System.Exception,System.String,System.Object[])")]
[assembly: SuppressMessage("Usage", "CA2254:La plantilla debe ser una expresión estática", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Logging.LoggerAdapter`1.LogDebug(System.String,System.Object[])")]

[assembly: SuppressMessage("Performance", "CA1848:Usar los delegados LoggerMessage", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Logging.LoggerAdapter`1.LogWarning(System.String,System.Object[])")]
[assembly: SuppressMessage("Performance", "CA1848:Usar los delegados LoggerMessage", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Logging.LoggerAdapter`1.LogInformation(System.String,System.Object[])")]
[assembly: SuppressMessage("Performance", "CA1848:Usar los delegados LoggerMessage", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Logging.LoggerAdapter`1.LogError(System.Exception,System.String,System.Object[])")]
[assembly: SuppressMessage("Performance", "CA1848:Usar los delegados LoggerMessage", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Logging.LoggerAdapter`1.LogDebug(System.String,System.Object[])")]

[assembly: SuppressMessage("Performance", "CA1822:Marcar miembros como static", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Repositories.RepositoryBase.ExecuteWithExceptionHandling``1(System.Func{System.Threading.Tasks.Task{``0}})~System.Threading.Tasks.Task{``0}")]
[assembly: SuppressMessage("Performance", "CA1822:Marcar miembros como static", Justification = "<pendiente>", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Infrastructure.Repositories.RepositoryBase.ExecuteWithExceptionHandling(System.Func{System.Threading.Tasks.Task})~System.Threading.Tasks.Task")]
