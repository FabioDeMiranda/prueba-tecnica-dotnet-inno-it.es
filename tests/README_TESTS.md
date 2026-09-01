# Tests

Batería de tests (xUnit) que describe el **comportamiento esperado** de la aplicación.

Sobre el proyecto tal cual está, **varios tests fallan**: te sirven como guía de qué no
funciona todavía. No necesitas que pasen todos — úsalos para orientarte, reproducir un
problema y comprobar tus arreglos.

## Ejecutarlos

Desde la raíz de la solución:

```bash
dotnet test
```

O solo este proyecto:

```bash
dotnet test tests/InterviewTest.Tests
```

Los tests instancian los handlers y repositorios directamente sobre una base de datos
SQLite en memoria, así que son rápidos y no necesitan que la API esté levantada.
