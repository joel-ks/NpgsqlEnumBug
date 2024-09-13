using Npgsql;

// Run create_types.sql on this database first to create the required DB types
const string dbConnectionString = ""; // Populate before running...

Console.WriteLine("Building data source");
var dataSourceBuilder = new NpgsqlDataSourceBuilder(dbConnectionString);

dataSourceBuilder.MapComposite<TestUdt>(TestUdt.DataTypeName);
dataSourceBuilder.MapEnum<TestEnum>();

var dataSource = dataSourceBuilder.Build();

Console.WriteLine("Opening connection");
using var conn = dataSource.OpenConnection();

Console.WriteLine("Using type in a query");

using var cmd = new NpgsqlCommand("select $1", conn)
{
    Parameters =
    {
        new NpgsqlParameter
        {
            Value = new TestUdt { EnumValue = TestEnum.Value2 }
        }
    }
};
_ = cmd.ExecuteScalar();

Console.WriteLine("Complete");

// POCOs for the UDTs
class TestUdt
{
    public const string DataTypeName = "test_udt";
    public TestEnum EnumValue { get; set; }
}

enum TestEnum
{
    Value1,
    Value2,
    Value3
}
