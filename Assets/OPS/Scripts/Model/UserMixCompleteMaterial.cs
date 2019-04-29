using System.Linq;
using UniRx;
using Zenject;

namespace OPS.Model
{

    public class UserMixCompleteMaterialDB : BaseSqliteModel<UserMixCompleteMaterialModel>
    {
        public override string DbName { get { return "user.sqlite3"; } }

        public override string TableName { get { return "user_mix_complete_materials"; } }

        [Inject]
        public MasterOptionDB _masterOptionDB;

        protected override UserMixCompleteMaterialModel DataRow2Model(DataRow DataRow)
        {
            var model = new UserMixCompleteMaterialModel();
            model.SetDB(this);
            model.id.Value = (int)DataRow["id"];
            model.user_mix_id.Value = (int)DataRow["user_mix_id"];
            model.select_agenda.Value = (int)DataRow["select_agenda"];
            model.master_option_id.Value = (int)DataRow["master_option_id"];
            model.rate.Value = (double)DataRow["rate"];
            return model;
        }

        protected override DataRow Model2DataRow(UserMixCompleteMaterialModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["user_mix_id"] = model.user_mix_id.Value;
            dataRow["select_agenda"] = model.select_agenda.Value;
            dataRow["master_option_id"] = model.master_option_id.Value;
            dataRow["rate"] = model.rate.Value;
            return dataRow;
        }
    }

    public class UserMixCompleteMaterialModel
    {
        UserMixCompleteMaterialDB _userMixCompleteMaterialDB;

        public void SetDB(UserMixCompleteMaterialDB userMixCompleteMaterialDB)
        {
            _userMixCompleteMaterialDB = userMixCompleteMaterialDB;
        }

        public IntReactiveProperty id = new IntReactiveProperty();
        public IntReactiveProperty user_mix_id = new IntReactiveProperty();
        public IntReactiveProperty select_agenda = new IntReactiveProperty();
        public IntReactiveProperty master_option_id = new IntReactiveProperty();
        public DoubleReactiveProperty rate = new DoubleReactiveProperty();

        public MasterOptionModel MasterOptionModel
        {
            get { return _userMixCompleteMaterialDB._masterOptionDB.Id(master_option_id.Value).First().Value; }
        }

        public void SelectAgenda()
        {
            var userMixAgendas = _userMixCompleteMaterialDB.Where("user_mix_id", user_mix_id.Value.ToString());
            foreach (var userMixAgenda in userMixAgendas)
            {
                if (MasterOptionModel.category_id.Value == userMixAgenda.Value.MasterOptionModel.category_id.Value)
                {
                    userMixAgenda.Value.select_agenda.Value = 0;
                }
            }
            select_agenda.Value = 1;
        }
    }

}