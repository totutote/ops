using System.Linq;
using UniRx;
using Zenject;

namespace OPS.Model
{

    public class MasterMixBonusDB : BaseSqliteModel<MasterMixBonusModel>
    {
        public override string DbName { get { return "master.sqlite3"; } }

        public override string TableName { get { return "master_mix_bonuses"; } }

        [Inject]
        public MasterOptionDB _masterOptionDB = null;

        [Inject]
        public MasterMixChainDB _masterMixChainDB = null;


        protected override MasterMixBonusModel DataRow2Model(DataRow DataRow)
        {
            var model = new MasterMixBonusModel();
            model.SetDB(this);
            model.id.Value = (int)DataRow["id"];
            model.master_mix_chain_id.Value = (int)DataRow["master_mix_chain_id"];
            model.master_option_id.Value = (int)DataRow["master_option_id"];
            model.rate.Value = (double)DataRow["rate"];
            return model;
        }

        protected override DataRow Model2DataRow(MasterMixBonusModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["master_mix_chain_id"] = model.master_mix_chain_id.Value;
            dataRow["master_option_id"] = model.master_option_id.Value;
            dataRow["rate"] = model.rate.Value;
            return dataRow;
        }

        public class Factory : PlaceholderFactory<MasterMixBonusDB>
        {
        }
    }

    public class MasterMixBonusModel
    {
        MasterMixBonusDB _masterMixBonusDB;

        public void SetDB(MasterMixBonusDB masterMixBonusDB)
        {
            _masterMixBonusDB = masterMixBonusDB;
        }

        public IntReactiveProperty id = new IntReactiveProperty();
        public IntReactiveProperty master_mix_chain_id = new IntReactiveProperty();
        public IntReactiveProperty master_option_id = new IntReactiveProperty();
        public DoubleReactiveProperty rate = new DoubleReactiveProperty();

        public MasterMixChainModel MasterMixChain
        {
            get { return _masterMixBonusDB._masterMixChainDB.Id(master_mix_chain_id.Value).First().Value; }
        }

        public MasterOptionModel MasterOptionModel
        {
            get { return _masterMixBonusDB._masterOptionDB.Id(master_option_id.Value).First().Value; }
        }
    }

}