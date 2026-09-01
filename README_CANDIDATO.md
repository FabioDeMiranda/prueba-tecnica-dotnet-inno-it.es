# Prueba técnica — API de Pedidos (.NET 8)

Bienvenido/a. Esto es una pequeña API REST de gestión de **pedidos** y **clientes**,
construida con .NET 8 y arquitectura por capas. Forma parte del día a día del proyecto:
un sistema que estamos migrando desde un monolito hacia servicios más pequeños.

> **No es un examen de memoria.** No pasa nada si no recuerdas la sintaxis exacta de una
> librería. Lo que nos interesa es **cómo piensas, cómo navegas el código y cómo abordas
> los problemas**. Ve narrando en voz alta lo que ves, lo que crees que pasa y qué harías.

---

## Qué hace la aplicación (funcionalidad esperada)

Es un CRUD sobre dos entidades relacionadas: un **cliente** (`Customer`) tiene muchos
**pedidos** (`Order`). Un pedido tiene un importe y un estado (`Pending`, `Confirmed`,
`Shipped`, `Cancelled`). Al crear un pedido, el sistema publica un evento en una cola
(aquí simulada) para que otros servicios reaccionen de forma asíncrona.

Endpoints REST:

| Verbo  | Ruta                        | Qué debería hacer                                  |
|--------|-----------------------------|----------------------------------------------------|
| GET    | `/api/customers`            | Lista de clientes, con su número de pedidos        |
| GET    | `/api/orders`               | Lista de pedidos, con el nombre del cliente        |
| GET    | `/api/orders/{id}`          | Un pedido por su id, con el nombre del cliente      |
| POST   | `/api/orders`               | Crea un pedido                                     |
| PUT    | `/api/orders/{id}/status`   | Cambia el estado de un pedido                      |

---

## Arquitectura y librerías

Solución dividida en cuatro capas:

- **Domain** — entidades, enums, interfaces de repositorio y eventos de dominio.
- **Application** — casos de uso con **MediatR** (Commands y Queries + sus Handlers),
  DTOs y mapeos. Se usa **AutoMapper** en unas partes y **Mapster** en otras.
- **Infrastructure** — **Entity Framework Core** (SQLite), repositorios y el publicador
  de eventos.
- **Api** — Web API REST; los controladores despachan a los handlers vía MediatR.

### ¿Por qué SQLite?

La base de datos es **SQLite** sobre un fichero local (`app.db`), que se crea y se
siembra con datos de ejemplo automáticamente al arrancar. Así el proyecto funciona sin
infraestructura externa, pero se comporta como una base de datos relacional de verdad
(a diferencia del proveedor InMemory, que no traduce SQL real ni valida relaciones).

---

## Requisitos

- **.NET 8 SDK** (`dotnet --version` debería devolver `8.x`).
- El editor que prefieras: Visual Studio, VS Code o Rider.
- No necesitas Docker ni ninguna base de datos instalada.

## Cómo arrancarlo

Desde la carpeta raíz de la solución:

```bash
dotnet restore
dotnet build
dotnet run --project src/Api
```

Al arrancar, abre en el navegador **Swagger UI**:

```
http://localhost:5080/swagger
```

También tienes un fichero [`requests.http`](requests.http) con peticiones de ejemplo si
prefieres probar desde el editor.

### Tests

Hay una batería de tests que describe el comportamiento esperado. **Varios fallan a
propósito**: úsalos como guía de qué no funciona todavía y para comprobar tus arreglos.

```bash
dotnet test
```

No necesitas que pasen todos; son una ayuda, no el objetivo.

---

## Tu tarea

El proyecto **tiene varios problemas** de distinta dificultad. Están repartidos por las
distintas capas y son del tipo de cosas que aparecen en el día a día.

1. **Arráncalo** y prueba los endpoints.
2. Ve **comentando en voz alta** qué ves, qué crees que falla y cómo lo abordarías.
3. Cuando detectes algo raro, intenta localizar **dónde** está y **por qué** ocurre.
   Si sabes arreglarlo, hazlo; si no, explica cómo lo investigarías.

> **No hace falta que lo termines todo ni que arregles todos los problemas.** Puedes
> saltar de uno a otro: son independientes. Nos interesa mucho más tu forma de razonar
> y de buscar información que llegar al final.

Si en algún momento no recuerdas cómo se hace algo con una librería concreta, dilo con
naturalidad y explica cómo lo buscarías: consultar la documentación es parte del trabajo.

¡Mucha suerte!
