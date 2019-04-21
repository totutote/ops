using OPS.Model;
using UnityEngine;
using Zenject;

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

    public void ShowPersistentDataPath()
    {
        Debug.Log(Application.persistentDataPath);
    }

    public void ViewUserMixCandidateMaterialOptionDB()
    {
        var db = new DatabaseConnection("user.sqlite3", "user_mix_candidate_material_options");
        Debug.Log(db.All());
    }

}
