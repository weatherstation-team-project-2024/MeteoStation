using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Extensions
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HypertableColumnAttribute : Attribute { }

    public static class TimeScaleExtensions
    {
        public static void ApplyHypertables(this DbContext context)
        {
            context.Database.ExecuteSqlRaw("CREATE EXTENSION IF NOT EXISTS timescaledb CASCADE;");

            var entityTypes = context.Model.GetEntityTypes();

            foreach (var entityType in entityTypes)
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.PropertyInfo.GetCustomAttribute(typeof(HypertableColumnAttribute)) != null)
                    {
                        var tableName = entityType.GetTableName();
                        var columnName = property.GetColumnName();
                        context.Database.ExecuteSqlRaw($"SELECT create_hypertable('\"{tableName}\"', '{columnName}');");
                    }
                }
            }
        }
    }
}