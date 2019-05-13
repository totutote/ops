using System.Linq;
using UniRx;
using Zenject;

namespace OPS.Model
{

    public class MasterAdditionalItemDB : BaseSqliteModel<MasterAdditionalItemModel>
    {
        public override string DbName { get { return "master.sqlite3"; } }

        public override string TableName { get { return "master_additional_items"; } }

        [Inject]
        public MasterOptionDB _masterOptionDB = null;

        protected override MasterAdditionalItemModel DataRow2Model(DataRow DataRow)
        {
            var model = new MasterAdditionalItemModel();
            model.SetDB(this);
            model.id.Value = (int)DataRow["id"];
            model.name.Value = (string)DataRow["name"];
            model.master_option_id.Value = (int)DataRow["master_option_id"];
            return model;
        }

        protected override DataRow Model2DataRow(MasterAdditionalItemModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["name"] = model.name.Value;
            dataRow["master_option_id"] = model.master_option_id.Value;
            return dataRow;
        }

        public class Factory : PlaceholderFactory<MasterAdditionalItemDB>
        {
        }

    }

    public class MasterAdditionalItemModel
    {
        MasterAdditionalItemDB _masterAdditionalItemDB;

        public void SetDB(MasterAdditionalItemDB masterAdditionalItemDB)
        {
            _masterAdditionalItemDB = masterAdditionalItemDB;
        }

        public IntReactiveProperty id = new IntReactiveProperty();
        public StringReactiveProperty name = new StringReactiveProperty();
        public IntReactiveProperty master_option_id = new IntReactiveProperty();

        public MasterOptionModel MasterOptionModel
        {
            get { return _masterAdditionalItemDB._masterOptionDB.Id(master_option_id.Value).First().Value; }
        }
    }

}