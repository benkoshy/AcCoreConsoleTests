// (C) Copyright 2016 by  
//
using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using System.IO;

// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(AcCoreConsoleTests.MyCommands))]

namespace AcCoreConsoleTests
{

    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    public class MyCommands
    {
        // The CommandMethod attribute can be applied to any public  member 
        // function of any public class.
        // The function should take no arguments and return nothing.
        // If the method is an intance member then the enclosing class is 
        // intantiated for each document. If the member is a static member then
        // the enclosing class is NOT intantiated.
        //
        // NOTE: CommandMethod has overloads where you can provide helpid and
        // context menu.

       
         [CommandMethod("MyCommands", "EntBreakup", CommandFlags.Modal)]
        public void EntBreakupMethod()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

             // obtain the selection set of all entities in the drawing:

            PromptSelectionResult psr = ed.SelectAll();
            if (psr.Status == PromptStatus.OK)
            {
                PrintSelectionSet("SelectAll", psr.Value);
            }

             // writing the successful text file
            // Create a string array with the lines of text
             string[] lines = { "First line", "Second line", "Third line" };

             string assemblyFolder = @"C:\Users\Koshy\Documents\Visual Studio 2013\Projects\AcCoreConsoleTests";

             // Write the string array to a new file named "WriteLines.txt".
             using (StreamWriter outputFile = new StreamWriter(assemblyFolder + @"\WriteLines.txt"))
             {
                 foreach (string line in lines)
                     outputFile.WriteLine(line);
             }
        }

         private void PrintSelectionSet(string title, SelectionSet ss)
         {
             Document doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
             Editor ed = doc.Editor;

             ed.WriteMessage(string.Format("{0} BreakupBegin", Environment.NewLine));

             foreach (ObjectId id in ss.GetObjectIds())
             {
                 ed.WriteMessage( string.Format("{0}{1}", Environment.NewLine, id.ObjectClass.Name)          );
             }

             ed.WriteMessage(string.Format("{0}BreakupEnd", Environment.NewLine));
         }
   

        // Application Session Command with localized name
        [CommandMethod("MyGroup", "MySessionCmd", "MySessionCmdLocal", CommandFlags.Modal | CommandFlags.Session)]
        public void MySessionCmd() // This method can have any name
        {
            // Put your command code here
        }

        // LispFunction is similar to CommandMethod but it creates a lisp 
        // callable function. Many return types are supported not just string
        // or integer.
        [LispFunction("MyLispFunction", "MyLispFunctionLocal")]
        public int MyLispFunction(ResultBuffer args) // This method can have any name
        {
            // Put your command code here

            // Return a value to the AutoCAD Lisp Interpreter
            return 1;
        }

    }

}
