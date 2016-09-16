using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using AbitExportProject.ActionMethods;
using AbitExportProject.Controllers;
using AbitExportProject.Data;
using AbitExportProject.ParsersToDB;
using Fdalilib;
using Fdalilib.Service;

namespace AbitExportProject
{
    internal class Program
    {   
        internal static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                LogWriter.WriteToNext();
                var actController = new ActionController();
                //вывести все возможные действия
                actController.PrintPossibleActions();

                Console.Write("\nВведите кодманду:");

                var action = Console.ReadLine();
                actController.MakeAction(action.Trim());

                Console.WriteLine("\nНажмите [ANY KEY] для продолжения или [ESCAPE] для выхода...");

            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }     
}
