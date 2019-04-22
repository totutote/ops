using System.Collections.Generic;
using UniRx;
using Zenject;

namespace OPS.Model
{

    public class UserMixDB : BaseSqliteModel<UserMixModel>
    {
        public override string DbName { get { return "user.sqlite3"; } }

        public override string TableName { get { return "user_mixes"; } }

        [Inject]
        public UserMixCandidateMaterialDB _userMixCandidateMaterialDB = null;

        [Inject]
        public MasterMixChainDB _masterMixChainDB = null;

        protected override UserMixModel DataRow2Model(DataRow DataRow)
        {
            var model = new UserMixModel();
            model.SetDB(this);
            model.id.Value = (int)DataRow["id"];
            model.name.Value = (string)DataRow["name"];
            return model;
        }

        protected override DataRow Model2DataRow(UserMixModel model)
        {
            var dataRow = new DataRow();
            dataRow["id"] = model.id.Value;
            dataRow["name"] = model.name.Value;
            return dataRow;
        }

        public class Factory : PlaceholderFactory<UserMixDB>
        {
        }
    }

    public class UserMixModel
    {
        UserMixDB _userMixDB;

        public void SetDB(UserMixDB userMixDB)
        {
            _userMixDB = userMixDB;
        }

        public IntReactiveProperty id = new IntReactiveProperty();
        public StringReactiveProperty name = new StringReactiveProperty();

        public Dictionary<int, UserMixCandidateMaterialModel> UserMixCandidateMaterialModel
        {
            get { return _userMixDB._userMixCandidateMaterialDB.Where("user_mix_id", id.Value.ToString()); }
        }

        public Dictionary<MasterOptionModel, float> MixOptionRate
        {
            get
            {
                Dictionary<MasterOptionModel, float> mixOptionRate = new Dictionary<MasterOptionModel, float>();
                foreach(var finalMasterMixChainModel in FinalMasterMixChainModels)
                {
                    if (!mixOptionRate.ContainsKey(finalMasterMixChainModel.Key.CreateMasterOptionModel))
                    {
                        mixOptionRate[finalMasterMixChainModel.Key.CreateMasterOptionModel] = finalMasterMixChainModel.Value.IncludeBonusRate;
                    }
                    else if(mixOptionRate[finalMasterMixChainModel.Key.CreateMasterOptionModel] < finalMasterMixChainModel.Value.IncludeBonusRate)
                    {
                        mixOptionRate[finalMasterMixChainModel.Key.CreateMasterOptionModel] = finalMasterMixChainModel.Value.IncludeBonusRate;
                    }
                }
                return mixOptionRate;
            }
        }

        private Dictionary<MasterMixChainModel, MasterMixChainModel> FinalMasterMixChainModels
        {
            get
            {
                Dictionary<MasterOptionModel, int> masterOptionModelsCount = MasterOptionModelsCount;
                Dictionary<MasterMixChainModel, MasterMixChainModel> finalMasterMixChains = new Dictionary<MasterMixChainModel, MasterMixChainModel>();
                foreach (var masterOptionModelCount in masterOptionModelsCount)
                {
                    foreach(var createMasterMixChain in masterOptionModelCount.Key.CreateMasterMixChains)
                    {
                        Dictionary<MasterOptionModel, int> cpyMasterOptionModelsCount = masterOptionModelsCount;
                        MasterMixChainModel finalMasterMixChainModel = createMasterMixChain.Value;
                        cpyMasterOptionModelsCount[finalMasterMixChainModel.MaterialMasterOptionModel] -= 1;
                        if (cpyMasterOptionModelsCount[finalMasterMixChainModel.MaterialMasterOptionModel] == 0)
                        {
                            cpyMasterOptionModelsCount.Remove(finalMasterMixChainModel.MaterialMasterOptionModel);
                        }
                        while(cpyMasterOptionModelsCount.ContainsKey(finalMasterMixChainModel.OverMasterMixChainModel.MaterialMasterOptionModel))
                        {
                            finalMasterMixChainModel = finalMasterMixChainModel.OverMasterMixChainModel;
                            cpyMasterOptionModelsCount[finalMasterMixChainModel.MaterialMasterOptionModel] -= 1;
                            if (cpyMasterOptionModelsCount[finalMasterMixChainModel.MaterialMasterOptionModel] == 0)
                            {
                                cpyMasterOptionModelsCount.Remove(finalMasterMixChainModel.MaterialMasterOptionModel);
                            }
                        }
                        finalMasterMixChains[createMasterMixChain.Value] = finalMasterMixChainModel;
                    }
                }
                return finalMasterMixChains;
            }
        }

        public Dictionary<MasterOptionModel, int> MasterOptionModelsCount
        {
            get
            {
                Dictionary<MasterOptionModel, int> masterOptionModelCount = new Dictionary<MasterOptionModel, int>();
                foreach (var userMixCandidateMaterialModel in UserMixCandidateMaterialModel)
                {
                    foreach (var materialMasterOptionCount in userMixCandidateMaterialModel.Value.MasterOptionModelCount)
                    {
                        masterOptionModelCount[materialMasterOptionCount.Key] += materialMasterOptionCount.Value;
                    }
                }
                return masterOptionModelCount;
            }
        }

    }

}