using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Poseidon.Web.Model
{
    public class PoseidonResult
    {
        public List<PoseidonDataItem> Items { get; set; }
        public string Id { get; set; }
        public int IdSession { get; set; }
        public string BasicResult { get; set; }
		public int LoadDuration { get; set; }
	

        public PoseidonResult()
        {
            Items = new List<PoseidonDataItem>();
        }

    }


    public class PoseidonDataItem
    {
		public string RegexToTest { get; set; }
		public string RegexResults { get; set; }
		public int IdSubRequestView { get; set; }
        public string Supplier { get; set; }
        public string IdRequest { get; set; }
        public string IdGame { get; set; }
        public int IdSubRequestGame { get; set; }
        public string PackageGame { get; set; }
        public string Result { get; set; }
        public string Request { get; set; }
        public string Method { get; set; }
        public List<PoseidonHeader> Headers { get; set; }
        public DateTime Stamp { get; set; }
        public bool IsWait { get; set; }
		public int MinWait { get; set; }
		public int MaxWait { get; set; }
        public string CurrentStep { get; set; }
        public string NextStep { get; set; }
        public string PostData { get; set; }
        public string Data1 { get; set; }
        public string Data2 { get; set; }
        public string Data3 { get; set; }
        public string Data4 { get; set; }
        public string Data5 { get; set; }
		public string Data6 { get; set; }
		public string Data7 { get; set; }
		public string Data8 { get; set; }
		public string Data9 { get; set; }
		public string Data10 { get; set; }
        public int LoopCount { get; set; }
        public int LoopIndex { get; set; }
		public bool IsFinished { get; set; }

        public PoseidonDataItem()
        {
            Headers = new List<PoseidonHeader>();
        }

    }

    public class PoseidonHeader
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public PoseidonHeader() { }

        public PoseidonHeader(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }


    public class PoseidonInput
    {
        public string Method { get; set; }
        public string GoogleAdId { get; set; }
        public string CountryCode { get; set; }
        public string IMEI { get; set; }
        public string MAC1 { get; set; }
        public string MAC2 { get; set; }
        public string Model { get; set; }
        public string OsVersion { get; set; }
        public string UdId { get; set; }
        public string Package { get; set; }

		public int Clicked { get; set; }
		public string CampaignPackage { get; set; }

        public int IdSupplier { get; set; }
        public int IdSession { get; set; }
        public int Failed { get; set; }




        public string IdInput { get; set; }
        public string Step { get; set; }
        public PoseidonDataItem GenericParameter { get; set; }


        public PoseidonInput()
        {
            GenericParameter = new PoseidonDataItem();
        }
    }

}
