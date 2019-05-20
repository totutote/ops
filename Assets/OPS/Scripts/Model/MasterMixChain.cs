using System;
using System.Collections.Generic;
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
        public MasterOptionDB _masterOptionDB = null;

        [Inject]
        public MasterMixBonusDB _masterMixBonusDB = null;

        protected override MasterMixChainModel DataRow2Model(DataRow DataRow)
        {
            var model = new MasterMixChainModel();
            model.SetDB(this);
            model.id.Value = (int)DataRow["id"];
            model.create_option_id.Value = (int?)DataRow["create_option_id"];
            model.material_option_id.Value = (int)DataRow["material_option_id"];
            model.over_mix_id.Value = (int?)DataRow["over_mix_id"];
            model.rate.Value = (double)DataRow["rate"];
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
        public ReactiveProperty<int?> create_option_id = new ReactiveProperty<int?>();
        public IntReactiveProperty material_option_id = new IntReactiveProperty();
        public ReactiveProperty<int?> over_mix_id = new ReactiveProperty<int?>();
        public DoubleReactiveProperty rate = new DoubleReactiveProperty();

        public MasterOptionModel CreateMasterOptionModel
        {
            get { return _masterMixChainDB._masterOptionDB.Id(create_option_id.Value).First().Value; }
        }

        public MasterOptionModel MaterialMasterOptionModel
        {
            get { return _masterMixChainDB._masterOptionDB.Id(material_option_id.Value).First().Value; }
        }

        public MasterMixChainModel OverMasterMixChainModel
        {
            get { return _masterMixChainDB.Where("over_mix_id", id.Value.ToString()).FirstOrDefault().Value; }
        }

        public double IncludeBonusRate(Dictionary<MasterOptionModel, int> masterOptionCount)
        {
            var bonuses = _masterMixChainDB._masterMixBonusDB.Where("master_mix_chain_id", id.Value.ToString());
            if (bonuses.Count == 0)
            {
                return rate.Value;
            }
            double mostRate = 0f;
            foreach (var bonus in bonuses)
            {
                if (!masterOptionCount.ContainsKey(bonus.Value.MasterOptionModel)) continue;
                if (mostRate < bonus.Value.rate.Value) mostRate = bonus.Value.rate.Value;
            }
            var includeBonusRate = rate.Value + mostRate;
            return includeBonusRate > 100f ? 100f : includeBonusRate;
        }
    }

}