using UniRx;

namespace OPS.Model
{

    public class MasterOptionParamBaseDB : BaseSqliteModel<MasterOptionParamBaseModel>
    {
        public override string DbName { get { return "master.sqlite3"; } }

        public override string TableName { get { return "master_option_param_bases"; } }

        protected override MasterOptionParamBaseModel DataRow2Model(DataRow DataRow)
        {
            var model = new MasterOptionParamBaseModel();
            model.id.Value = (int)DataRow["id"];
            model.name.Value = (string)DataRow["name"];
            return model;
        }

        protected override DataRow Model2DataRow(MasterOptionParamBaseModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["name"] = model.name.Value;
            return dataRow;
        }
    }

    public class MasterOptionParamBaseModel
    {
        public IntReactiveProperty id = new IntReactiveProperty();
        public StringReactiveProperty name = new StringReactiveProperty();
    }

}