using System.Linq;
using UniRx;
using Zenject;

namespace OPS.Model
{

    public class MasterMixChainDB : BaseSqliteModel<MasterMixChainModel>
    {
        public override string DbName { get { return "master.sqlite3"; } }

        public override string TableName { get { return "master_mix_chains"; } }

        [Inject]
        public MasterOptionDB _masterOptionDB;

        [Inject]
        public MasterMixBonusDB _masterMixBonusDB;

        protected override MasterMixChainModel DataRow2Model(DataRow DataRow)
        {
            var model = new MasterMixChainModel();
            model.SetDB(this);
            model.id.Value = (int)DataRow["id"];
            model.create_option_id.Value = (int)DataRow["create_option_id"];
            model.material_option_id.Value = (int)DataRow["material_option_id"];
            model.over_mix_id.Value = (int)DataRow["over_mix_id"];
            model.rate.Value = (float)DataRow["rate"];
            return model;
        }

        protected override DataRow Model2DataRow(MasterMixChainModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["create_option_id"] = model.create_option_id.Value;
            dataRow["material_option_id"] = model.material_option_id.Value;
            dataRow["over_mix_id"] = model.over_mix_id.Value;
            dataRow["rate"] = model.rate.Value;
            return dataRow;
        }

        public class Factory : PlaceholderFactory<MasterMixChainDB>
        {
        }
    }

    public class MasterMixChainModel
    {
        MasterMixChainDB _masterMixChainDB;

        public void SetDB(MasterMixChainDB masterMixChainDB)
        {
            _masterMixChainDB = masterMixChainDB;
        }

        public IntReactiveProperty id = new IntReactiveProperty();
        public IntReactiveProperty create_option_id = new IntReactiveProperty();
        public IntReactiveProperty material_option_id = new IntReactiveProperty();
        public IntReactiveProperty over_mix_id = new IntReactiveProperty();
        public FloatReactiveProperty rate = new FloatReactiveProperty();

        public MasterOptionModel CreateMasterOptionModel
        {
            get { return _masterMixChainDB._masterOptionDB.Where("id", create_option_id.Value.ToString()).First().Value; }
        }

        public MasterOptionModel MaterialMasterOptionModel
        {
            get { return _masterMixChainDB._masterOptionDB.Where("id", material_option_id.Value.ToString()).First().Value; }
        }

        public MasterMixChainModel OverMasterMixChainModel
        {
            get { return _masterMixChainDB.Where("over_mix_id", id.Value.ToString()).First().Value; }
        }

        public float IncludeBonusRate
        {
            get
            {
                var bonuses = _masterMixChainDB._masterMixBonusDB.Where("master_mix_chain_id", id.Value.ToString());
                if (bonuses == null)
                {
                    return rate.Value;
                }
                float mostRate = 0f;
                foreach (var bonus in bonuses)
                {
                    if(mostRate < bonus.Value.rate.Value) mostRate = bonus.Value.rate.Value;
                }
                return mostRate;
            }
        }
    }

}