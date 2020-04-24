using ASSETKKF_ADO.Mssql.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Constant
{
    public class StaticValue
    {

     
        private List<ASSETKKF_MODEL.Data.Mssql.Asset.muAccessToken> _muAccessToken;
        public List<ASSETKKF_MODEL.Data.Mssql.Asset.muAccessToken> muAccessToken { get { return _muAccessToken; } }

        private List<ASSETKKF_MODEL.Data.Mssql.Asset.muToken> _muToken;
        public List<ASSETKKF_MODEL.Data.Mssql.Asset.muToken> muToken { get { return _muToken; } }

        private List<ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET> _zUser;
        public List<ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET> zUser { get { return _zUser; } }

        //private Boolean _RuoubsSyncActive;
        //public Boolean RuoubsSyncActive { get { return _RuoubsSyncActive; } set { this._RuoubsSyncActive = value; } }
        
        private static StaticValue instant { get; set; }
        private StaticValue()
        {
        }

        public static StaticValue GetInstant()
        {
            if (instant == null) instant = new StaticValue();
            return instant;
        }

        public void LoadInstantAll()
        {
            this.AccessKey();
            this.TokenKey();
            this.UserData();
        }

        public void AccessKey()
        {
            this._muAccessToken?.Clear();
            this._muAccessToken = muAccessTokenAdo.GetInstant().ListActive();
        }

        public void TokenKey()
        {
            this._muToken?.Clear();
            this._muToken = muTokenAdo.GetInstant().ListActive();
        }

        public void UserData()
        {
            this._zUser?.Clear();
            this._zUser = ASSETKKF_ADO.Mssql.Asset.STUSERASSETAdo.GetInstant().Search(new ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET());
        }

        public string GetUserDetail(string userCode)
        {
            return this.zUser.Where(x => x.UCODE == userCode).Select(x => x.UCODE + " : " + x.OFNAME).FirstOrDefault();
        } 
         
        
    }
}
