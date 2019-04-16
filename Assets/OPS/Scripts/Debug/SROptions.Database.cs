public partial class SROptions
{

    public void ForceUpdateUserSchema()
    {
       var db = new SqliteDatabase("user.sqlite3");
       db.ForceUpdateDatabaseSchema();
    }

}
