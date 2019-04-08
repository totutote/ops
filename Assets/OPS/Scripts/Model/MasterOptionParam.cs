using UniRx;
using Zenject;
using System.Linq;

namespace OPS.Model
{

    public class MasterOptionParamDB : BaseSqliteModel<MasterOptionParamModel>
    {
        public override string DbName { get { return "master.sqlite3"; } }

        public override string TableName { get { return "master_option_params"; } }

        public MasterOptionDB _masterOptionDB;

        public MasterOptionParamDB(MasterOptionDB masterOptionDB)
        {
            _masterOptionDB = masterOptionDB;
        }

        protected override MasterOptionParamModel DataRow2Model(DataRow DataRow)
        {
            var model = new MasterOptionParamModel();
            model.SetDB(this);
            model.id.Value = (int)DataRow["id"];
            model.option_id.Value = (int)DataRow["option_id"];
            model.base_id.Value = (int)DataRow["base_id"];
            model.value.Value = (int)DataRow["value"];
            return model;
        }

        protected override DataRow Model2DataRow(MasterOptionParamModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["option_id"] = model.option_id.Value;
            dataRow["base_id"] = model.base_id.Value;
            dataRow["value"] = model.value.Value;
            return dataRow;
        }

        public class Factory : PlaceholderFactory<MasterOptionParamDB>
        {
        }
    }

    public class MasterOptionParamModel
    {
        MasterOptionParamDB _masterOptionParamDB;

        public IntReactiveProperty id = new IntReactiveProperty();
        public IntReactiveProperty option_id = new IntReactiveProperty();
        public IntReactiveProperty base_id = new IntReactiveProperty();
        public IntReactiveProperty value = new IntReactiveProperty();

        public void SetDB(MasterOptionParamDB _masterOptionPramDB)
        {
            _masterOptionParamDB = _masterOptionPramDB;
        }

        public MasterOptionModel MasterOptionModel
        {
            get { return _masterOptionParamDB._masterOptionDB.Where("id", option_id.Value.ToString()).First().Value; }
        }
    }

}