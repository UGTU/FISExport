using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Fdalilib.Service
{
    /// <summary>
    /// Контракт интерфейса сервиса
    /// </summary>
    [ContractClassFor(typeof(IFisProxyService))]
    class FisProxyServiceContract : IFisProxyService
    {
        public XElement GetDictionaries(XElement authData)
        {
            Contract.Requires(authData !=null);
            Contract.Ensures(Contract.Result<XElement>()!=null);
            throw new NotImplementedException();
        }

        public XElement GetDictionaryDetails(XElement dictionaryContent)
        {
            Contract.Requires(dictionaryContent != null);
            Contract.Ensures(Contract.Result<XElement>() != null);
            throw new NotImplementedException();
        }

        public XElement GetSingleCheckApp(XElement xElement)
        {
            Contract.Requires(xElement != null);
            Contract.Ensures(Contract.Result<XElement>() != null);
            throw new NotImplementedException();
        }

        public XElement ImportSingle(XElement xElement)
        {
            Contract.Requires(xElement != null);
            Contract.Ensures(Contract.Result<XElement>() != null);
            throw new NotImplementedException();
        }

        public XElement ValidatePack(XElement xElement)
        {
            Contract.Requires(xElement != null);
            Contract.Ensures(Contract.Result<XElement>() != null);
            throw new NotImplementedException();
        }

        public XElement ImportPack(XElement xElement)
        {
            Contract.Requires(xElement != null);
            Contract.Ensures(Contract.Result<XElement>() != null);
            throw new NotImplementedException();
        }

        public XElement ImportPackResult(XElement xElement)
        {
            Contract.Requires(xElement != null);
            Contract.Ensures(Contract.Result<XElement>() != null);
            throw new NotImplementedException();
        }

        public XElement DeleteData(XElement xElement)
        {
            Contract.Requires(xElement != null);
            Contract.Ensures(Contract.Result<XElement>() != null);
            throw new NotImplementedException();
        }

        public XElement DeleteDataResult(XElement xElement)
        {
            Contract.Requires(xElement != null);
            Contract.Ensures(Contract.Result<XElement>() != null);
            throw new NotImplementedException();
        }

        public XElement GetCheckPack(XElement xElement)
        {
            Contract.Requires(xElement != null);
            Contract.Ensures(Contract.Result<XElement>() != null);
            throw new NotImplementedException();
        }

        public XElement GetUniversityInfo(XElement xElement)
        {
            Contract.Requires(xElement != null);
            Contract.Ensures(Contract.Result<XElement>() != null);
            throw new NotImplementedException();
        }

    }
}
