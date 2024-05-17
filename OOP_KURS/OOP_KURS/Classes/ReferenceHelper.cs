using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DocCreator
{
    // Работа со справочниками
    public static class ReferenceHelper
    {

        public static Customer Organization = new Customer();

        private static readonly Dictionary<Type, object> Components = new Dictionary<Type, object>
        {
            { typeof(Customer),          new CustomerReference() },
            { typeof(TypeDocument),      new TypeReference() },
            { typeof(Bank),              new BankReference() },
            { typeof(ProductAndService), new ProductAndServiceReference() },
            { typeof(Document),          new DocumentReference() },
            { typeof(OrganizationForm),  new OrganizationFormReference() },
        };

        private static readonly Dictionary<string, Type> AltComponentsName = new Dictionary<string, Type> { };

        public static void Add(dynamic Elem) => InvokeMethodByElem(Elem, "AddToList");

        public static void Delete(dynamic Elem) => InvokeMethodByElem(Elem, "DeleteFromList");

        private static void InvokeMethodByElem(dynamic Elem, string MethodName)
        {
            try
            {
                dynamic Instance = Components[Elem.GetType()];
                MethodInfo Method = Instance.GetType().GetMethod(MethodName);
                Method?.Invoke(Instance, new object[] { Elem });
            }
            catch { }
        }

        public static dynamic GetElementsByRefName(string RefName)
        {
            return InvokeMethod(RefName, "GetElements");
        }

        public static dynamic InvokeMethod(string RefName, string MethodName)
        {
            try
            {
                MethodInfo Method = Components[AltComponentsName[RefName]].GetType().GetMethod(MethodName);
                return Method?.Invoke(Components[AltComponentsName[RefName]], new object[] { });
            }
            catch
            {
                return null;
            }
        }

        public static void InvokeMethod(string RefName, string MethodName, dynamic Params)
        {
            try
            {
                MethodInfo Method = Components[AltComponentsName[RefName]].GetType().GetMethod(MethodName);
                Method?.Invoke(Components[AltComponentsName[RefName]], new object[] { Params });
            }
            catch
            {

            }
        }

        static ReferenceHelper()
        {
            FillAltNameDict();
            InitTypeDocs();
        }

        private static void FillAltNameDict()
        {
            foreach (var Comp in Components)
            {
                AltComponentsName.Add(Comp.Key.Name, Comp.Key);
            }
        }
        private static void InitTypeDocs()
        {
            Add(new TypeDocument { Name = "Счет", ExcelTemplatePath = Environment.CurrentDirectory.ToString() + @"\TempAcc.xlsx", FileName = "Счет" } );
            Add(new TypeDocument { Name = "Акт выполненных работ", ExcelTemplatePath = Environment.CurrentDirectory.ToString() + @"\TempAct.xlsx", FileName = "Акт" });

            Organization.Bank.Name = "Поволжский банк ПАО Банк № 1 России";
            Organization.Bank.BIK = 043601607;
            Organization.CompanyRepresentative.FirstName = "Иван";
            Organization.CompanyRepresentative.LastName = "Иванов";
            Organization.CompanyRepresentative.Patronymic = "Иванович";
            Organization.CompanyRepresentative.Position = "Директор";
            Organization.CorrespondentAccount = "90304696734701740985";
            Organization.PaymentAccount = "21254892798962765272";
            Organization.INN = 211289602651;
            Organization.KPP = 420503011;
            Organization.LegalAddress.City = "Набережные Челны";
            Organization.LegalAddress.AppartNumber = "23";
            Organization.LegalAddress.House = "9";
            Organization.LegalAddress.ShortRegion = "РТ";
            Organization.LegalAddress.Region = "Республика Татарстан";
            Organization.LegalAddress.Street = "Пушкина";
            Organization.Name = "Ремонт24";
            Organization.Phone = "89235613795";

            OrganizationForm OOO = new OrganizationForm { Name = "Общество с ограниченной ответственностью", ShortName = "ООО" };
            Organization.Form = OOO;

            Add(OOO);
            Add(new OrganizationForm { Name = "Индивидуальный предприниматель", ShortName = "ИП" });
            Add(new OrganizationForm { Name = "Публичное акционерное общество", ShortName = "ПАО" });



            Customer Cust1 = new Customer();
            Cust1.Name = "Про Колёса";
            Cust1.Form = OOO;

            Cust1.Bank.Name = "Банк Первый";
            Cust1.Bank.BIK = 043601607;
            Cust1.CompanyRepresentative.FirstName = "Сидоров";
            Cust1.CompanyRepresentative.LastName = "Илья";
            Cust1.CompanyRepresentative.Patronymic = "Иванович";
            Cust1.CompanyRepresentative.Position = "Директор";
            Cust1.CorrespondentAccount = "66204696734701740985";
            Cust1.PaymentAccount = "89254892798451765272";
            Cust1.INN = 211289602651;
            Cust1.KPP = 420503011;
            Cust1.LegalAddress.City = "Москва";
            Cust1.LegalAddress.AppartNumber = "26";
            Cust1.LegalAddress.House = "224";
            Cust1.LegalAddress.ShortRegion = "Москва";
            Cust1.LegalAddress.Region = "Москва";
            Cust1.LegalAddress.Street = "Комсомольская";
            Cust1.Phone = "89565613235";

            Add(Cust1);




        }
    }

}
