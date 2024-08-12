using System.Data;
using Dapper;

namespace BiodataManagement.Data.Configuration;
public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly> // Dapper handler for DateOnly
{
    public override DateOnly Parse(object value) => DateOnly.FromDateTime((DateTime)value);

    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.DbType = DbType.Date;
        parameter.Value = value;
    }
}

public class TimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly> // Dapper handler for TimeOnly
{
    public override TimeOnly Parse(object value)
    {
        if (value.GetType() == typeof(DateTime))
        {
            return TimeOnly.FromDateTime((DateTime)value);
        }
        else if (value.GetType() == typeof(TimeSpan))
        {
            return TimeOnly.FromTimeSpan((TimeSpan)value);
        }
        return default;
    }

    public override void SetValue(IDbDataParameter parameter, TimeOnly value)
    {
        parameter.DbType = DbType.Time;
        parameter.Value = value;
    }
}
