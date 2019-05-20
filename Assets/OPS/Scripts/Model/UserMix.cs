using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
        public UserMixCandidateMaterialOptionDB _userMixCandidateMaterialOptionDB = null;

        [Inject]
        public MasterMixChainDB _masterMixChainDB = null;

        [Inject]
        public MasterAdditionalItemDB _masterAdditionalItemDB = null;

        [Inject]
        public UserMixCompleteMaterialDB _userMixCompleteMaterialDB = null;

        [Inject]
        public UserMixKeyValueDB _userMixKeyValueDB = null;

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

        public Dictionary<int, UserMixCompleteMaterialModel> UserMixCompleteMaterialModels
        {
            get { return _userMixDB._userMixCompleteMaterialDB.Where("user_mix_id", id.Value.ToString()); }
        }

        public Dictionary<int, UserMixCompleteMaterialModel> UserMixCompleteMaterialSelectAgendaModels
        {
            get
            {
                var searchCollection = new NameValueCollection { { "user_mix_id", id.Value.ToString() } };
                var addAbaleOptionCount = _userMixDB._userMixCompleteMaterialDB.New().ExtraRateTable.Count;
                foreach (var index in Enumerable.Range(1, addAbaleOptionCount))
                {
                    searchCollection.Add("select_agenda", index.ToString());
                }
                return _userMixDB._userMixCompleteMaterialDB.Where(searchCollection);
            }
        }

        public void SortSelectAgenda()
        {
           var userMixCompleteMaterialSelectAgendaModels = UserMixCompleteMaterialSelectAgendaModels;
           int i = 1;
           foreach (var userMixCompleteMaterialSelectAgendaModel in userMixCompleteMaterialSelectAgendaModels)
           {
               userMixCompleteMaterialSelectAgendaModel.Value.select_agenda.Value = i++;
               _userMixDB._userMixCompleteMaterialDB.Save(userMixCompleteMaterialSelectAgendaModel.Value);
           }
        }

        public UserMixCandidateMaterialModel BodyUserMixCandidateMaterialModel
        {
            get { return _userMixDB._userMixCandidateMaterialDB.Where("sort_index", "0").First().Value; }
        }

        public UserMixKeyValueModel UserMixAdditionalItem
        {
            get { return _userMixDB._userMixKeyValueDB.Where(new NameValueCollection { { "user_mix_id", id.Value.ToString() }, { "key", "\"master_additional_item_id\"" } }).FirstOrDefault().Value; }
        }

        public UserMixKeyValueModel UserMixSameNameBonusItem
        {
            get { return _userMixDB._userMixKeyValueDB.Where(new NameValueCollection { { "user_mix_id", id.Value.ToString() }, { "key", "\"same_name_bonus\"" } }).FirstOrDefault().Value; }
        }

        public UserMixKeyValueModel UserMixPeriodRateBonusKeyValue
        {
            get { return _userMixDB._userMixKeyValueDB.Where(new NameValueCollection { { "user_mix_id", id.Value.ToString() }, { "key", "\"period_rate_bonus\"" } }).FirstOrDefault().Value; }
        }

        public MasterOptionModel AdditionalItemMasterOptionModel
        {
            get
            {
                var userAdditionalItem = UserMixAdditionalItem;
                if (userAdditionalItem == null) return default(MasterOptionModel);
                return _userMixDB._masterAdditionalItemDB.Id(int.Parse(userAdditionalItem.value.Value)).First().Value.MasterOptionModel;
            }
        }

        public void SaveOrCreateAdditionalItem(int additionalItemId)
        {
            _userMixDB._userMixKeyValueDB.SaveOrCreateInteger(id.Value, "master_additional_item_id", additionalItemId);
        }

        public void SaveOrCreateSameNameBonus(int sameNameBonus)
        {
            _userMixDB._userMixKeyValueDB.SaveOrCreateInteger(id.Value, "same_name_bonus", sameNameBonus);
        }

        public void SaveOrCreatePeriodRateBonus(int pariodRateBonus)
        {
            _userMixDB._userMixKeyValueDB.SaveOrCreateInteger(id.Value, "pariod_rate_bonus", pariodRateBonus);
        }

        public void DestroyCompleteModel()
        {
            foreach (var completeMaterialModel in _userMixDB._userMixCompleteMaterialDB.Where("user_mix_id", id.Value.ToString()))
            {
                _userMixDB._userMixCompleteMaterialDB.Delete(completeMaterialModel.Value);
            }
        }

        public Dictionary<MasterOptionModel, double> MixOptionRate
        {
            get
            {
                Dictionary<MasterOptionModel, double> mixOptionRate = new Dictionary<MasterOptionModel, double>();
                var additionalItem = AdditionalItemMasterOptionModel;
                if (additionalItem != null) mixOptionRate[additionalItem] = 100f;
                var sameNameBonus = UserMixSameNameBonusItem;
                var periodRateBonus = UserMixPeriodRateBonusKeyValue;
                Dictionary<MasterOptionModel, int> masterOptionModelsCount = MasterOptionModelsCount;
                foreach (var finalMasterMixChainModel in FinalMasterMixChainModels)
                {
                    var includeBonusRate = finalMasterMixChainModel.Value.IncludeBonusRate(masterOptionModelsCount);
                    if (includeBonusRate <= 0) continue;
                    if (sameNameBonus != null && int.Parse(sameNameBonus.value.Value) == 1)
                    {
                        includeBonusRate = Math.Round(includeBonusRate * 1.15f, MidpointRounding.AwayFromZero);
                    }
                    if (periodRateBonus != null)
                    {
                        includeBonusRate += int.Parse(periodRateBonus.value.Value) * 5;
                    }
                    if (!mixOptionRate.ContainsKey(finalMasterMixChainModel.Key.CreateMasterOptionModel))
                    {
                        mixOptionRate[finalMasterMixChainModel.Key.CreateMasterOptionModel] = includeBonusRate;
                    }
                    else if (mixOptionRate[finalMasterMixChainModel.Key.CreateMasterOptionModel] < includeBonusRate)
                    {
                        mixOptionRate[finalMasterMixChainModel.Key.CreateMasterOptionModel] = includeBonusRate;
                    }
                }
                return mixOptionRate.OrderBy(x => x.Key.category_id.Value).OrderBy(x => x.Key.id.Value).ToDictionary(x => x.Key, x => x.Value);
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
                    foreach (var createMasterMixChain in masterOptionModelCount.Key.NotNullCreateOptionChains)
                    {
                        Dictionary<MasterOptionModel, int> cpyMasterOptionModelsCount = new Dictionary<MasterOptionModel, int>(masterOptionModelsCount);
                        MasterMixChainModel finalMasterMixChainModel = createMasterMixChain.Value;
                        MasterOptionModel finalKeyMaterialOptionModel = finalMasterMixChainModel.MaterialMasterOptionModel;
                        cpyMasterOptionModelsCount[finalKeyMaterialOptionModel] -= 1;
                        if (cpyMasterOptionModelsCount[finalKeyMaterialOptionModel] == 0)
                        {
                            cpyMasterOptionModelsCount.Remove(finalKeyMaterialOptionModel);
                        }
                        var finalOverMasterMixChainModel = finalMasterMixChainModel.OverMasterMixChainModel;
                        while (finalOverMasterMixChainModel != null && cpyMasterOptionModelsCount.ContainsKey(finalOverMasterMixChainModel.MaterialMasterOptionModel))
                        {
                            finalMasterMixChainModel = finalOverMasterMixChainModel;
                            finalKeyMaterialOptionModel = finalMasterMixChainModel.MaterialMasterOptionModel;
                            cpyMasterOptionModelsCount[finalKeyMaterialOptionModel] -= 1;
                            if (cpyMasterOptionModelsCount[finalKeyMaterialOptionModel] == 0)
                            {
                                cpyMasterOptionModelsCount.Remove(finalKeyMaterialOptionModel);
                            }
                            finalOverMasterMixChainModel = finalMasterMixChainModel.OverMasterMixChainModel;
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
                List<int> materialIdList = new List<int>();
                foreach (var userMixCandidateMaterialModel in UserMixCandidateMaterialModel)
                {
                    materialIdList.Add(userMixCandidateMaterialModel.Value.id.Value);
                }
                foreach (var materialMasterOptionCount in _userMixDB._userMixCandidateMaterialOptionDB.MaterialIdListSelect(materialIdList))
                {
                    var keyMasterOptionModel = materialMasterOptionCount.Value.MasterOptionModel;
                    if (masterOptionModelCount.ContainsKey(keyMasterOptionModel))
                    {
                        masterOptionModelCount[keyMasterOptionModel] += 1;
                    }
                    else
                    {
                        masterOptionModelCount[keyMasterOptionModel] = 1;
                    }
                }
                return masterOptionModelCount;
            }
        }

    }

}