function runSqlScript($file, $ds, $db, $username, $password)
{
	if($username)
    {
        [string]::Format("sqlcmd -I -i {0} -U {1} -P ***** -d {2}", $file, $username, $db);
        sqlcmd -I -i "$file" -U "$username" -P "$password" -S "$ds" -d "$db" -V 11 -l 20
    }
    else
    {
        [string]::Format("sqlcmd -I -i {0} -E -d {1}", $file, $db);
        sqlcmd -I -i "$file" -E -S "$ds" -d "$db" -V 11 -l 20
    }
}

export-modulemember -function runSqlScript
