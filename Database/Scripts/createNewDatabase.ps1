param
(
    [string]$DBConnectionString,
    [string]$DatabaseName
)

$ConnectionString
$NewDatabaseName

if ($DBConnectionString)
{
    $ConnectionString = $DBConnectionString
}
else
{
    $ConnectionString = "Data Source=.;Initial Catalog=master;Integrated Security=True;"
}

if ($DatabaseName)
{
    $NewDatabaseName = $DatabaseName
}
else
{
    $NewDatabaseName = "OnlineGym"
}

$con = New-Object Data.SqlClient.SqlConnection;
$con.ConnectionString = $ConnectionString;
$con.Open();

# create the database.
$sql = "CREATE DATABASE [$NewDatabaseName] COLLATE SQL_Latin1_General_CP1_CI_AS;"
$cmd = New-Object Data.SqlClient.SqlCommand $sql, $con;
$cmd.ExecuteNonQuery();     
Write-Host "Database $NewDatabaseName is created!";

# close & clear all objects.
$cmd.Dispose();
$con.Close();
$con.Dispose();