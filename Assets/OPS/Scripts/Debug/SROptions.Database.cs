public partial class SROptions
{

    public void ForceUpdateMasterSchema()
    {
       var db = new SqliteDatabase("master.sqlite3");
       db.ForceUpdateDatabaseSchema();
    }

    public void ForceUpdateUserSchema()
    {
       var db = new SqliteDatabase("user.sqlite3");
       db.ForceUpdateDatabaseSchema();
    }

}
