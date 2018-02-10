#region Using
using Library.ViewModel.Items;
using Microsoft.Practices.Unity;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Library.Model.Items;
using UYVMS.App_Start;
using Library.Service.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DocumentFormat.OpenXml.Drawing;
using Library.Core;
using Library.Crosscutting.Security;

#endregion

namespace UYVMS.WebServices.Items
{
    /// <summary>
    /// Summary description for WebItemService
    /// </summary>
    [
       ScriptService,
       WebService(Namespace = "http://tempuri.org/"),
       WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)
   ]
    public class WebItemService : WebService
    {
        readonly IUnityContainer Unitycontainer = UnityConfig.GetConfiguredContainer();
        private readonly IItemService _itemService;
        private readonly IItemMasterService _itemMasterService;
        public WebItemService()
        {
            _itemService = Unitycontainer.Resolve<IItemService>();
            _itemMasterService = Unitycontainer.Resolve<IItemMasterService>();
        }

        //[
        //WebMethod(EnableSession = true),
        //ScriptMethod(ResponseFormat = ResponseFormat.Json)
        //]
        //public string ExcelBulkUpload(ItemNewViewModel item)
        //{
        //    return this._itemService.ExcelBulkUpload(item.ToEntity());
        //}

        [
            WebMethod,
            ScriptMethod(UseHttpGet = true)
        ]
        public string GetAutoId()
        {
            return new JavaScriptSerializer().Serialize(_itemService.GetAutoId());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemTypeId"></param>
        /// <returns></returns>
        [
            WebMethod,
            ScriptMethod(UseHttpGet = true)
        ]
        public string GetItemTypeLastInsertedId(int itemTypeId)
        {
            return new JavaScriptSerializer().Serialize(_itemService.GetItemTypeLastInsertedId(itemTypeId));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [
            WebMethod,
            ScriptMethod(UseHttpGet = true)
        ]
        public string GetCode(string code)
        {
            return new JavaScriptSerializer().Serialize(_itemService.CheckingUniqueCode(code));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [
            WebMethod,
            ScriptMethod(UseHttpGet = true)
        ]
        public string GetName(string name)
        {
            return new JavaScriptSerializer().Serialize(_itemService.CheckingUniqueName(name));
        }
        [
     WebMethod,
     ScriptMethod(UseHttpGet = true)
]
        public string GetAllItemList()
        {
            // return new JavaScriptSerializer().Serialize(_itemService.GetAllItemList());

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            return js.Serialize(_itemService.GetAllItemList());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [
             WebMethod,
             ScriptMethod(UseHttpGet = true)
        ]
        public string GetItemList()
        {
            return new JavaScriptSerializer().Serialize(_itemService.GetItemList());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [
             WebMethod,
             ScriptMethod(UseHttpGet = true)
        ]
        public decimal GetItemClosing(string id)
        {
            return _itemService.GetItemClosing(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [
             WebMethod,
             ScriptMethod(UseHttpGet = true)
        ]
        public string GetMushok11GaItemDetailList()
        {
            return new JavaScriptSerializer().Serialize(_itemService.GetMushok11GaItemDetailList());
        }

        [
             WebMethod,
             ScriptMethod(UseHttpGet = true)
        ]
        public decimal GetItemUnitPriceByItemId(string itemId)
        {
            return _itemService.GetItemUnitPriceByItemId(itemId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemDetailCode"></param>
        /// <returns></returns>
        [
           WebMethod,
           ScriptMethod(UseHttpGet = true)
        ]
        public string GetItemUnitPriceAndItemTypeIdByItemId(string itemId)
        {

            decimal unitPrice;
            int itemTypeId;
            string uomId;
            _itemService.GetItemUnitPriceAndItemTypeIdByItemId(itemId, out unitPrice, out itemTypeId, out uomId);

            var data = new
            {
                UnitPrice = unitPrice,
                ItemTypeId = itemTypeId,
                UOMId = uomId
            };
            return new JavaScriptSerializer().Serialize(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemDetailCode"></param>
        /// <returns></returns>
        [
           WebMethod,
           ScriptMethod(ResponseFormat = ResponseFormat.Json)
        ]
        public string GetItemDetailUnitPriceAndUOMIdByItemDetailCode(string itemDetailCode)
        {
            decimal unitPrice;
            string uomId;
            _itemService.GetItemDetailUnitPriceAndUOMIdByItemDetailCode(itemDetailCode, out unitPrice, out uomId);
            var data = new
            {
                UnitPrice = unitPrice,
                UOMId = uomId
            };
            return new JavaScriptSerializer().Serialize(data);
        }

        [
           WebMethod,
          ScriptMethod(UseHttpGet = true)
        ]
        public string GetItemForPurchaseByItemId(string itemId)
        {
            decimal unitPrice;
            string uomId;
            int itemTypeId;
            decimal perOfVat;
            decimal perOfReVat;
            int traifTypeId;
            string hsCode;
            _itemService.GetItemForPurchaseByItemId(itemId, out unitPrice, out uomId,
                out itemTypeId, out perOfVat, out perOfReVat, out traifTypeId, out hsCode);
            var data = new
            {
                UnitPrice = unitPrice,
                UOMId = uomId,
                ItemTypeId = itemTypeId,
                PerOfVat = perOfVat,
                PerOfReVat = perOfReVat,
                TraifTypeId = traifTypeId,
                HSCode = hsCode
            };
            return new JavaScriptSerializer().Serialize(data);
        }
        [
     WebMethod(EnableSession = true),
     ScriptMethod(ResponseFormat = ResponseFormat.Json)
 ]
        public string Save(ItemViewModel item)
        {
            return _itemService.AddOrUpdate(item.ToEntity());
        }
        [
WebMethod(EnableSession = true),
ScriptMethod(ResponseFormat = ResponseFormat.Json)
]
        public string UnitPriceSave(ItemViewModel item)
        {
            return _itemService.UnitPriceSave(item.ToEntity());
        }
        [
           WebMethod(EnableSession = true),
           ScriptMethod(UseHttpGet = true)
       ]
        public ItemViewModel Get(string itemId)
        {

            try
            {
                Item objItem = _itemService.Get(r => r.Id == itemId);
                ItemViewModel itemVM = new ItemViewModel();
                itemVM.Id = objItem.Id;
                itemVM.ItemId = objItem.Id;
                itemVM.ItemCode = objItem.Code;
                itemVM.ItemName = objItem.Name;
                itemVM.UOMId = objItem.UOMId;
                itemVM.TariffTypeId = objItem.TariffTypeId;
                itemVM.ItemTypeId = objItem.ItemTypeId;
                itemVM.MaterialTypeId = objItem.MaterialTypeId;
                itemVM.ItemCategoryId = objItem.ItemCategoryId;
                itemVM.ItemSubCategoryId = objItem.ItemSubCategoryId;
                itemVM.ItemCodeSeq = objItem.ItemCodeSeq;
                itemVM.ItemHSCode = objItem.ItemHSCode;
                itemVM.ItemDescription = objItem.ItemDescription;
                DateTime dt = Convert.ToDateTime(objItem.EntryDate);
                itemVM.EntryDate = dt.ToString("dd-MMM-yyyy");
                itemVM.UnitPrice = objItem.UnitPrice;
                itemVM.OpeningQty = objItem.OpeningQty;
                itemVM.OpeningValue = objItem.OpeningValue;
                itemVM.ClosingLabel = objItem.ClosingLabel;
                itemVM.PhysicalClosing = objItem.PhysicalClosing;
                itemVM.PhysicalValue = objItem.PhysicalValue;
                itemVM.IsActive = objItem.IsActive;
                itemVM.Accept = objItem.Accept;
                return itemVM;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        [WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Accept(string itemId)
        {
            _itemService.Accept(itemId);
        }
        [WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AcceptForSingleCompany(string itemId)
        {
            _itemService.AcceptForSingleCompany(itemId);
        }
        [
       WebMethod,
       ScriptMethod(UseHttpGet = true)
        ]
        public string GetServiceItemList()
        {
            return new JavaScriptSerializer().Serialize(_itemService.GetServiceItemList());
        }

        [
       WebMethod,
       ScriptMethod(UseHttpGet = true)
        ]
        public string GetNonServiceItemList()
        {
            return new JavaScriptSerializer().Serialize(_itemService.GetNonServiceItemList());
        }



        //AutoComplete Item List
        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetAutoCompleteItemMasterList(string prefix)
        {
            CustomIdentity identity = (CustomIdentity)Thread.CurrentPrincipal.Identity;

            var results = _itemMasterService.GetAll( x => x.CompanyId == identity.CompanyId && x.BranchId == identity.BranchId && (x.ItemCode.Contains(prefix) || x.ItemName.Contains(prefix))).ToArray();
            List<string> itemsList = results.Select(result => string.Format("{0}<>{1}", "(" + result.ItemCode + ")" + result.ItemName, result.ItemId)).ToList();            
            return itemsList.ToArray();
            
        }
    }
}
