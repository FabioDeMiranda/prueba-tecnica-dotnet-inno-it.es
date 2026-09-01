using InterviewTest.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace InterviewTest.Tests.Support;

/// <summary>
/// Base de datos SQLite en memoria compartida por una conexión abierta. Cada
/// <see cref="NewContext"/> devuelve un DbContext nuevo (sin tracking heredado) sobre la
/// misma BD, lo que permite comprobar que los cambios se persisten de verdad.
/// </summary>
public sealed class SqliteTestDb : IDisposable
{
    private readonly SqliteConnection _connection;

    public SqliteTestDb()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        using var ctx = NewContext();
        ctx.Database.EnsureCreated();
    }

    public AppDbContext NewContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        return new AppDbContext(options);
    }

    public void Dispose() => _connection.Dispose();
}
