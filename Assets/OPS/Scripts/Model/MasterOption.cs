using UniRx;

namespace OPS.Model
{

    public class MasterOptionDB : BaseSqliteModel<MasterOptionModel>
    {
        public override string DbName { get { return "master.sqlite3"; } }

        public override string TableName { get { return "master_options"; } }

        protected override MasterOptionModel DataRow2Model(DataRow DataRow)
        {
            var model = new MasterOptionModel();
            model.id.Value = (int)DataRow["id"];
            model.name.Value = (string)DataRow["name"];
            model.category_id.Value = (int)DataRow["category_id"];
            return model;
        }

        protected override DataRow Model2DataRow(MasterOptionModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["name"] = model.name.Value;
            dataRow["category_id"] = model.category_id.Value;
            return dataRow;
        }
    }

    public class MasterOptionModel
    {
        public IntReactiveProperty id = new IntReactiveProperty();
        public StringReactiveProperty name = new StringReactiveProperty();
        public IntReactiveProperty category_id = new IntReactiveProperty();
    }

}