using System.Collections.Generic;
using System.Linq;

namespace AbitExportProject.Data
{

    partial class Person
    {
        const int EGE = 5;
        const int EGE_OUT = 8;

        public List<EgeDocument> EgeDocs
        { 
            get
            {
                var listEge = new List<EgeDocument>();
                var mContext = new UGTUDataDataContext();
                foreach (var app in mContext.ABIT_postups.Where(x => x.nCode == nCode))
                {
                    foreach (var eD in app.ABIT_Vstup_exams.Where(y => (y.ik_sdach == EGE) || (y.ik_sdach == EGE_OUT))) //для всех ЕГЭ-документов
                        if (eD.cosenka != null)
                        {
                            // nSert = eD.NNvedom.Substring(eD.NNvedom.IndexOf('-') - 2, 15);
                            var indStr = eD.NNvedom.IndexOf('-');
                            if (indStr <= 0) continue;
                            var nSert = eD.NNvedom.Substring(eD.NNvedom.IndexOf('-') - 2, 15);
                            if (listEge.All(z => z.DocumentNumber != nSert))
                                //если еще не было сертификата с данным номером
                            {
                                listEge.Add(new EgeDocument() //добавляем сертификат
                                {
                                    DocumentNumber = nSert, //eD.NNvedom,
                                    DocumentYear = "20" + nSert.Substring(nSert.Length-2, 2),
                                    Subjects = new List<Subject>()
                                });
                            }
                            if (
                                listEge.Single(s => s.DocumentNumber == nSert)
                                    .Subjects.All(s => s.SubjectId != eD.ABIT_Disc.ik_FB))
                            {
                                listEge.Single(s => s.DocumentNumber == nSert)
                                    .Subjects.Add(new Subject() //добавляем предмет с оценкой
                                    {
                                        SubjectId = (int) eD.ABIT_Disc.ik_FB,
                                        Value = (int) eD.cosenka
                                    });
                            }
                        }
                }

                return listEge.Count > 0 ? listEge : null;
            }
                }
    }
}
        