using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;

namespace OPS.Model
{

    public class MasterOptionDB : BaseSqliteModel<MasterOptionModel>
    {
        public override string DbName { get { return "master.sqlite3"; } }

        public override string TableName { get { return "master_options"; } }

        [Inject]
        public MasterOptionCategoryDB _masterOptionCategoryDB = null;

        [Inject]
        public MasterMixChainDB _masterMixChainDB = null;

        protected override MasterOptionModel DataRow2Model(DataRow DataRow)
        {
            var model = new MasterOptionModel();
            model.SetDB(this);
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

        public class Factory : PlaceholderFactory<MasterOptionDB>
        {
        }
    }

    public class MasterOptionModel
    {
        MasterOptionDB _masterOptionDB;

        public void SetDB(MasterOptionDB masterOptionDB)
        {
            _masterOptionDB = masterOptionDB;
        }

        public IntReactiveProperty id = new IntReactiveProperty();
        public StringReactiveProperty name = new StringReactiveProperty();
        public IntReactiveProperty category_id = new IntReactiveProperty();

        public MasterOptionCategoryModel MasterOptionCategoryModel
        {
            get { return _masterOptionDB._masterOptionCategoryDB.Id(category_id.Value).First().Value; }
        }

        public Dictionary<int, MasterMixChainModel> CreateMasterMixChains
        {
            get { return _masterOptionDB._masterMixChainDB.Where("create_option_id", id.Value.ToString()); }
        }

        public Dictionary<int, MasterMixChainModel> NotNullCreateOptionChains
        {
            get
            {
                Dictionary<int, MasterMixChainModel> notNullCreateOptions = new Dictionary<int, MasterMixChainModel>();
                var hitMaterialOptions = _masterOptionDB._masterMixChainDB.Where("material_option_id", id.Value.ToString());
                foreach (var hitMaterialOption in hitMaterialOptions)
                {
                    if (hitMaterialOption.Value.create_option_id.Value == null) continue;
                    notNullCreateOptions[hitMaterialOption.Key] = hitMaterialOption.Value;
                }
                return notNullCreateOptions;
            }
        }
    }

}