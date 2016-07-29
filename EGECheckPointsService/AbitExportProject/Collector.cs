using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using AbitExportProject.Data;
using Fdalilib.Actions2015;

namespace AbitExportProject
{
    /// <summary>
    /// Производит сбор комплексных элементов для отправки пакетов
    /// </summary>
    public static class Collector
    {
        const int IsNetwork = 9;                      
        const int SPO = 5;
        const int Ochn = 1;
        const int Zaoch = 2;
        const int OZaoch = 7;
        const int EGE = 5;
        const int Budjet = 1;
        const int CKP = 2;
    }
}
