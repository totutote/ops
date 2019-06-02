using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;

namespace OPS.Model
{

    public class MasterOptionCategoryDB : BaseSqliteModel<MasterOptionCategoryModel>
    {
        public override string DbName { get { return "master.sqlite3"; } }

        public override string TableName { get { return "master_option_categories"; } }

        [Inject]
        public MasterOptionDB _masterOptionDB = null;

        protected override MasterOptionCategoryModel DataRow2Model(DataRow DataRow)
        {
            var model = new MasterOptionCategoryModel();
            model.SetDB(this);
            model.id.Value = (int)DataRow["id"];
            model.name.Value = (string)DataRow["name"];
            return model;
        }

        protected override DataRow Model2DataRow(MasterOptionCategoryModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["name"] = model.name.Value;
            return dataRow;
        }

        public List<int> SpecialOptionIds = new List<int>() { 58, 59, 60 };

        public class Factory : PlaceholderFactory<MasterOptionCategoryDB>
        {
        }

    }

    public class MasterOptionCategoryModel
    {
        MasterOptionCategoryDB _masterOptionCategoryDB;

        public void SetDB(MasterOptionCategoryDB masterOptionCategoryDB)
        {
            _masterOptionCategoryDB = masterOptionCategoryDB;
        }

        public IntReactiveProperty id = new IntReactiveProperty();
        public StringReactiveProperty name = new StringReactiveProperty();

        public MasterOptionModel MasterOptionModel
        {
            get { return _masterOptionCategoryDB._masterOptionDB.Id(id.Value).First().Value; }
        }
    }

}