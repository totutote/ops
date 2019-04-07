using UniRx;

namespace OPS.Model
{

    public class MasterOptionParamDB : BaseSqliteModel<MasterOptionParamModel>
    {
        public override string DbName { get { return "master.sqlite3"; } }

        public override string TableName { get { return "master_option_params"; } }

        protected override MasterOptionParamModel DataRow2Model(DataRow DataRow)
        {
            var model = new MasterOptionParamModel();
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
    }

    public class MasterOptionParamModel
    {
        public IntReactiveProperty id = new IntReactiveProperty();
		public IntReactiveProperty option_id = new IntReactiveProperty();
		public IntReactiveProperty base_id = new IntReactiveProperty();
		public IntReactiveProperty value = new IntReactiveProperty();
    }

}