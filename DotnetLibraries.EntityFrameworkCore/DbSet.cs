using System.Reflection;
using Microsoft.Data.SqlClient;

namespace DotnetLibraries.EntityFrameworkCore;

public sealed class DbSet<TEntity>
    where TEntity : class, new()
{
    private readonly string _connectinString;
    private readonly string _tableName;
    public DbSet(DbContext dbContext, string tableName)
    {
        _connectinString = dbContext.Options.ConnectionString;
        _tableName = tableName;
    }
    public List<TEntity> ToList()
    {
        List<TEntity> list = new();
        var conn = new SqlConnection(_connectinString);
        conn.Open();

        using var cmd = new SqlCommand($"SELECT * FROM [{_tableName}]", conn);
        using var r = cmd.ExecuteReader();

        var ordinals = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < r.FieldCount; i++) ordinals[r.GetName(i)] = i;

        var props = typeof(TEntity)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite)
            .ToArray();

        while (r.Read())
        {
            var entity = new TEntity();
            foreach (var p in props)
            {
                if (!ordinals.TryGetValue(p.Name, out var idx)) continue;
                if (r.IsDBNull(idx)) continue;

                var raw = r.GetValue(idx);
                var dest = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;

                object? value;
                if (dest.IsAssignableFrom(raw.GetType()))
                    value = raw;
                else
                    continue;

                p.SetValue(entity, value);
            }
            list.Add(entity);
        }
        conn.Close();

        return list;
    }
}
